using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Human
{
    public class BodyPartManager : MonoBehaviour
    {
        public static Action OnGameOverEvent;
        public static Action<char> OnWinEvent;
        public static Action<List<BodyPart>> OnScenarioGeneratedEvent;
        [SerializeField] private List<BodyPart> bodyParts, transferBodyPartsAlternatives;
        [SerializeField] private int topScore, maxBlood;
        private BloodMonitor _bloodMonitor;
        private int _currentBlood;
        private int _currentScore;
        private bool _skinFlapsIsOpen;
        private int _survivalBloodAmount;
        private int _totalTransplants;
        private List<BodyPart> _transferBodyParts;

        private void Awake()
        {
            _bloodMonitor = FindObjectOfType<BloodMonitor>();
            _currentBlood = maxBlood;
            _survivalBloodAmount = (int) (maxBlood * 0.6f);
            _totalTransplants = GameManager.GetOrganAmount();
            _transferBodyParts = new List<BodyPart>();
        }

        private void Start()
        {
            SkinFlaps.OnOpenFlapEvent += () => _skinFlapsIsOpen = true;
            SkinFlaps.OnCloseFlapEvent += () => _skinFlapsIsOpen = false;
            BodyPart.OnToolUsedEvent += (organ, s) => CheckWinConditions();
            SkinFlaps.OnCloseFlapEvent += CheckWinConditions;
            GenerateScenario();
            StartCoroutine(BloodControl());
        }

        private void OnDestroy()
        {
            SkinFlaps.OnOpenFlapEvent = null;
            SkinFlaps.OnCloseFlapEvent = null;
            BodyPart.OnToolUsedEvent = null;
            SkinFlaps.OnCloseFlapEvent = null;
        }

        private void CheckWinConditions()
        {
            if (!bodyParts.Union(_transferBodyParts).Where(o => o.isBad == false).All(o => o.IsAttached()) ||
                _skinFlapsIsOpen) return;
            OnWinEvent?.Invoke(GetTotalScore());
        }

        private void GenerateScenario()
        {
            while (_transferBodyParts.Count < _totalTransplants)
            {
                var organ = bodyParts[Random.Range(0, bodyParts.Count)];
                if (_transferBodyParts.Any(o => o.GetPartName() == organ.GetPartName())) continue;
                organ.SetAsBadOrgan();
                _transferBodyParts.Add(transferBodyPartsAlternatives.Find(o => o.GetPartName() == organ.GetPartName()));
            }

            _transferBodyParts.ForEach(o => o.gameObject.SetActive(true));
            OnScenarioGeneratedEvent?.Invoke(_transferBodyParts);
        }

        private IEnumerator BloodControl()
        {
            while (_currentBlood > _survivalBloodAmount)
            {
                yield return new WaitForSeconds(1f);
                _currentBlood -= GetBloodLostAmount();
                _bloodMonitor.OnBloodLost?.Invoke(_currentBlood, _survivalBloodAmount);
            }

            OnGameOverEvent?.Invoke();
        }

        private void GetBodyPartScore(BodyPart bodyPart)
        {
            var score = 100f - 100 * Mathf.Clamp01(bodyPart.GetGoalDistance() - 0.1f);
            _currentScore = (int) (_currentScore * (score / 100f));
        }

        private char GetTotalScore()
        {
            _currentScore = topScore;
            foreach (var b in bodyParts.Union(_transferBodyParts).ToArray().Where(b => b.isBad == false))
                GetBodyPartScore(b);
            _currentScore = (int) (_currentScore * Mathf.Clamp01((float) _currentBlood / maxBlood + 0.05f));
            if (_currentScore > 95) return 'A';
            if (_currentScore > 85) return 'B';
            if (_currentScore > 75) return 'C';
            return _currentScore > 65 ? 'D' : 'E';
        }

        private int GetBloodLostAmount()
        {
            return bodyParts
                .Where(b => b.IsAttached() == false)
                .Where(b => !_transferBodyParts.Any(t => t.GetPartName() == b.GetPartName() && t.IsAttached()))
                .Sum(b => b.GetBloodLostAmount());
        }
    }
}