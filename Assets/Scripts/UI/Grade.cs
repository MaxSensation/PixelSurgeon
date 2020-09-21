using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Grade : MonoBehaviour
{
    [SerializeField] private List<Sprite> grades;
    [SerializeField] private GameObject grade, uiGameObject;
    private Image _image;
    private void Start()
    {
        _image = grade.GetComponent<Image>();
        OrganManager.OnTransplantSuccessfulEvent += UpdateGrade;
        OrganManager.OnLostToMuchBloodEvent += () => UpdateGrade('F');
    }

    private void OnDestroy()
    {
        OrganManager.OnTransplantSuccessfulEvent -= UpdateGrade;
        OrganManager.OnLostToMuchBloodEvent -= () => UpdateGrade('F');
    }

    private void UpdateGrade(char gradeChar)
    {
        _image.sprite = grades.Find(g => g.name == gradeChar.ToString());
        uiGameObject.SetActive(true);
    }
}
