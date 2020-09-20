using System.Collections.Generic;
using Human;
using TMPro;
using UnityEngine;

public class PatientChart : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        OrganManager.OnScenarioGenerated += OnScenarioGenerated;
    }

    private void OnScenarioGenerated(List<Organ> organs)
    {
        var newText = "";
        organs.ForEach(o => newText += $"{o.GetOrganName()} Transplant\n");
        text.text = newText;
    }
}
