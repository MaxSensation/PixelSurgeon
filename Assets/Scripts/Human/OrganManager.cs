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
    [SerializeField] private int maxScorePerOrgan = default, startBlood = default, totalOrganTransplants = default;
    private int _survivalBloodAmount;
    private int _currentBlood;
    private BloodMonitor _bloodMonitor;
    private int _currentScore;

    public static Action OnLostToMuchBloodEvent;

    private void Start()
    {
        _bloodMonitor = FindObjectOfType<BloodMonitor>();
        _currentBlood = startBlood;
        _survivalBloodAmount = (int) (startBlood * 0.6f);
        StartCoroutine(BloodControl());
        GenerateScenario();
    }

    private void GenerateScenario()
    {
        while (transferOrgans.Count < totalOrganTransplants)
        {
            var organ = inBodyOrgans[Random.Range(0, inBodyOrgans.Count)];
            if (transferOrgans.Any(o => o.GetOrganName() == organ.GetOrganName())) continue;
            organ.badOrgan = true;
            transferOrgans.Add(transferOrgansAlternatives.Find(o => o.GetOrganName() == organ.GetOrganName()));
        }
        transferOrgans.ForEach(o => o.gameObject.SetActive(true));
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
    
    /*

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
    */
    
    private int GetBloodLostAmount()
    {
        var currentLostBlood = 0;
        var organs = inBodyOrgans.Union(transferOrgans).GroupBy(o => o).Where(i => i.Count() > 1).Select(y => y.Key).ToList();
        return currentLostBlood;
    }    
}
