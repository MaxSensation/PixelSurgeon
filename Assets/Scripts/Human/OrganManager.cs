using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Human;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrganManager : MonoBehaviour
{
    [SerializeField] private List<Organ> inBodyOrgans = default, transferOrgans = default, transferOrgansAlternatives = default;
    [SerializeField] private int maxScorePerOrgan = default, startBlood = default; 
    private int _totalOrganTransplants;
    private bool _skinFlapsIsOpen;
    private int _survivalBloodAmount;
    private int _currentBlood;
    private BloodMonitor _bloodMonitor;
    private int _currentScore;
    private Coroutine _coroutine;

    public static Action OnLostToMuchBloodEvent;
    public static Action<char> OnTransplantSuccessfulEvent;
    public static Action<List<Organ>> OnScenarioGeneratedEvent;

    private void Awake()
    {
        SkinFlaps.OnOpenFlapEvent += () => _skinFlapsIsOpen = true;
        SkinFlaps.OnCloseFlapEvent += () => _skinFlapsIsOpen = false;
        Organ.OnOrganModifiedEvent += (organ, s) => CheckWinConditions();
        SkinFlaps.OnCloseFlapEvent += CheckWinConditions;
        _totalOrganTransplants = GameManager.GetOrganAmount();
        _bloodMonitor = FindObjectOfType<BloodMonitor>();
        _currentBlood = startBlood;
        _survivalBloodAmount = (int) (startBlood * 0.6f);
        _coroutine = StartCoroutine(BloodControl());
        _skinFlapsIsOpen = false;
    }

    private void Start()
    {
        GenerateScenario();
    }

    private void OnDestroy()
    {
        SkinFlaps.OnOpenFlapEvent -= () => _skinFlapsIsOpen = true;
        SkinFlaps.OnCloseFlapEvent -= () => _skinFlapsIsOpen = false;
        Organ.OnOrganModifiedEvent -= (organ, s) => CheckWinConditions();
        SkinFlaps.OnCloseFlapEvent -= CheckWinConditions;
        StopCoroutine(_coroutine);
    }

    private void CheckWinConditions()
    {
        if (!inBodyOrgans.Union(transferOrgans).Where(o => o.badOrgan == false).All(o => o.IsAttached()) || _skinFlapsIsOpen) return;
        OnTransplantSuccessfulEvent?.Invoke(GetScore());
    }

    private void GenerateScenario()
    {
        while (transferOrgans.Count < _totalOrganTransplants)
        {
            var organ = inBodyOrgans[Random.Range(0, inBodyOrgans.Count)];
            if (transferOrgans.Any(o => o.GetOrganName() == organ.GetOrganName())) continue;
            organ.badOrgan = true;
            transferOrgans.Add(transferOrgansAlternatives.Find(o => o.GetOrganName() == organ.GetOrganName()));
        }
        transferOrgans.ForEach(o => o.gameObject.SetActive(true));
        OnScenarioGeneratedEvent?.Invoke(transferOrgans);
    }

    private IEnumerator BloodControl()
    {
        while (_currentBlood > _survivalBloodAmount)
        {
            yield return new WaitForSeconds(1f);
            _currentBlood -= GetBloodLostAmount();
            _bloodMonitor.OnBloodLost?.Invoke(_currentBlood, _survivalBloodAmount);   
        }
        OnLostToMuchBloodEvent?.Invoke();
    }

    private void CalculateOrganScorePercentage(Organ organ)
    {
        var score = 100f - 100 * Mathf.Clamp01(organ.GetGoalDistance() - 0.1f);
        _currentScore = (int) (_currentScore * (score / 100f));
        
    }

    private char GetScore()
    {
        _currentScore = maxScorePerOrgan;
        var allOrgans = inBodyOrgans.Union(transferOrgans).ToArray();
        foreach (var organ in allOrgans.Where(o => o.badOrgan == false))
            CalculateOrganScorePercentage(organ);
        _currentScore = (int) (_currentScore * Mathf.Clamp01((float)_currentBlood / startBlood + 0.05f));
        if (_currentScore > 95) return 'A';
        if (_currentScore > 85) return 'B';
        if (_currentScore > 75) return 'C';
        if (_currentScore > 65) return 'D';
        return _currentScore > 55 ? 'E' : 'F';
    }

    private int GetBloodLostAmount()
    {
        return inBodyOrgans
            .Where(o => o.IsAttached() == false)
            .Where(organ => !transferOrgans.Any(t => t.GetOrganName() == organ.GetOrganName() && t.IsAttached()))
            .Sum(organ => organ.GetBloodLostAmount());
    }    
}
