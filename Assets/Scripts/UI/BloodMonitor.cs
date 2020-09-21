using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BloodMonitor : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI bloodAmountText;
        public Action<int, int> OnBloodLost;


        private void Start()
        {
            OnBloodLost += SetBloodAmount;
        }

        private void OnDestroy()
        {
            OnBloodLost -= SetBloodAmount;
        }

        private void SetBloodAmount(int amount, int minimum)
        {
            bloodAmountText.SetText("cur: " + amount + "ml" + "\n" + "min: " + minimum + "ml");
        }
    }
}