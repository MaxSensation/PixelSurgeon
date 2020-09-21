﻿using System;
using System.Collections.Generic;
using Human;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PatientChart : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI text;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        OrganManager.OnScenarioGeneratedEvent += OnScenarioGenerated;
    }

    private void OnDestroy()
    {
        OrganManager.OnScenarioGeneratedEvent -= OnScenarioGenerated;
    }

    private void OnScenarioGenerated(List<Organ> organs)
    {
        var newText = "";
        organs.ForEach(o => newText += $"{o.GetOrganName()} Transplant\n");
        text.text = newText;
    }

    public void OnPointerClick(PointerEventData eventData) => _animator.SetTrigger("Clicked");
    
}
