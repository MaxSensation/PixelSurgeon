using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BloodMonitor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bloodAmountText = default;
    public Action<int, int> OnBloodLost;


    private void Start()
    {
        OnBloodLost += SetBloodAmount;
    }

    private void SetBloodAmount(int amount, int minimum)
    {
        bloodAmountText.SetText("cur: "+ amount + "ml" + "\n" + "min: " + minimum + "ml");
    }
}
