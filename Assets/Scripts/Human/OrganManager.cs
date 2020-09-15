using System;
using System.Collections;
using System.Collections.Generic;
using Human;
using UnityEngine;

public class OrganManager : MonoBehaviour
{
    [SerializeField] private List<Organ> organs;
    [SerializeField] private int maxScorePerOrgan;
    private int _currentScore;

    private int GetOrganScore()
    {
        _currentScore = maxScorePerOrgan;
        foreach (var organ in organs)
        {
            var percentage = 100f - 100 * Mathf.Clamp01(organ.GetGoalDistance()-0.1f); 
            Debug.Log(organ.name + " PositionPlacement: " + percentage + " from perfect position.");
            _currentScore = (int) (_currentScore * (percentage / 100f));
        }
        return _currentScore;
    }
}
