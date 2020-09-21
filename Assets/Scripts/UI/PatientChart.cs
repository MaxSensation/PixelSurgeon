using System.Collections.Generic;
using Human;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class PatientChart : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI text;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            BodyPartManager.OnScenarioGeneratedEvent += OnScenarioGenerated;
        }

        private void OnDestroy()
        {
            BodyPartManager.OnScenarioGeneratedEvent -= OnScenarioGenerated;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _animator.SetTrigger("Clicked");
        }

        private void OnScenarioGenerated(List<BodyPart> organs)
        {
            var newText = "";
            organs.ForEach(o => newText += $"{o.GetPartName()} Transplant\n");
            text.text = newText;
        }
    }
}