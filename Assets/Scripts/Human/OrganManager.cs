using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Human;
using UnityEngine;

public class OrganManager : MonoBehaviour
{
    [SerializeField] private List<Organ> goodOrgans = default, badOrgans = default;
    [SerializeField] private int maxScorePerOrgan = default;
    [SerializeField] private int startBlood = default;
    private int _currentBlood;
    private BloodMonitor _bloodMonitor;
    private int _currentScore;

    private void Start()
    {
        _bloodMonitor = FindObjectOfType<BloodMonitor>();
        _currentBlood = startBlood;
        StartCoroutine(BloodControl());
    }

    private IEnumerator BloodControl()
    {
        while (_currentBlood > 0)
        {
            yield return new WaitForSeconds(1f);
            _currentBlood -= GetBloodLostAmount();
            _bloodMonitor.OnBloodLost?.Invoke(_currentBlood);   
        }
    }

    private int GetOrganScore()
    {
        _currentScore = maxScorePerOrgan;
        foreach (var organ in goodOrgans)
        {
            var percentage = 100f - 100 * Mathf.Clamp01(organ.GetGoalDistance()-0.1f); 
            Debug.Log(organ.name + " PositionPlacement: " + percentage + " from perfect position.");
            _currentScore = (int) (_currentScore * (percentage / 100f));
        }
        return _currentScore;
    }

    private int GetBloodLostAmount()
    {
        var lostBlood = 0;
        float goodOrganPercentage = 0, badOrganPercentage = 0;
        foreach (var goodOrgan in goodOrgans)
        {
            goodOrganPercentage = 100f - 100 * Mathf.Clamp01(goodOrgan.GetGoalDistance()-0.1f);
            if (badOrgans.Any(badorgan => badorgan.getOrganName() == goodOrgan.getOrganName()))
            {
                var organ = badOrgans.Where(badOrgan => badOrgan.getOrganName() == goodOrgan.name).ToArray()[0];
                badOrganPercentage = 100f - 100 * Mathf.Clamp01(organ.GetGoalDistance()-0.1f);
                if (!(goodOrganPercentage > 90 || badOrganPercentage > 90))
                    lostBlood += goodOrgan.GetBloodLostAmount();
            }
            else
            {
                if (!(goodOrganPercentage > 90))
                    lostBlood += goodOrgan.GetBloodLostAmount();   
            }
        }

        return lostBlood;
    }
}
