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
    private int _survivalBloodAmount;
    private int _currentBlood;
    private BloodMonitor _bloodMonitor;
    private int _currentScore;

    public static Action OnGoodOrgansNotAttachedEvent, OnLostToMuchBloodEvent;

    private void Start()
    {
        _bloodMonitor = FindObjectOfType<BloodMonitor>();
        _currentBlood = startBlood;
        _survivalBloodAmount = (int) (startBlood * 0.6f);
        StartCoroutine(BloodControl());
    }

    private IEnumerator BloodControl()
    {
        while (_currentBlood > _survivalBloodAmount)
        {
            yield return new WaitForSeconds(1f);
            _currentBlood -= GetBloodLostAmount();
            _bloodMonitor.OnBloodLost?.Invoke(_currentBlood, _survivalBloodAmount);   
        }
        Debug.Log("Human died of bloodloss");
        OnLostToMuchBloodEvent?.Invoke();
    }

    public int GetOrganScore()
    {
        _currentScore = maxScorePerOrgan;
        foreach (var goodOrgan in goodOrgans)
        {
            var goodOrganPercentage = 100f - 100 * Mathf.Clamp01(goodOrgan.GetGoalDistance()-0.1f);
            if (goodOrgan.IsAttached())
            {
                _currentScore = (int) (_currentScore * (goodOrganPercentage / 100f));
                Debug.Log(goodOrgan.GetOrganName() + " PositionPlacement: " + goodOrganPercentage + "%");
            }
            else
            {
                Debug.Log(goodOrgan.GetOrganName() + " is not attached!");
                _currentScore = 0;
                OnGoodOrgansNotAttachedEvent?.Invoke();
            }
        }
        Debug.Log("Human has " + _currentBlood + "ml blood Left");
        Debug.Log("Total Score: " + _currentScore);
        return _currentScore;
    }

    private int GetBloodLostAmount()
    {
        var currentLostBlood = 0;
        foreach (var goodOrgan in goodOrgans)
        {
            if (!goodOrgan.IsAttached() && badOrgans.Any(badorgan => badorgan.GetOrganName() == goodOrgan.GetOrganName()))
            {
                var badOrgan = badOrgans.Where(organ => organ.GetOrganName() == goodOrgan.name).ToArray()[0];
                if (!badOrgan.IsAttached()) 
                    currentLostBlood += goodOrgan.GetBloodLostAmount();
            }
            else if (!goodOrgan.IsAttached())
                currentLostBlood += goodOrgan.GetBloodLostAmount();
        }
        return currentLostBlood;
    }
}
