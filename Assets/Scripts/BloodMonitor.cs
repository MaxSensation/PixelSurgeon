using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BloodMonitor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bloodAmountText = default;
    public Action<int> OnBloodLost;


    private void Start()
    {
        OnBloodLost += SetBloodAmount;
    }

    private void SetBloodAmount(int amount)
    {
        bloodAmountText.SetText(amount + "ml");
    }
}
