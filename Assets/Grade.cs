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
        Debug.Log("Test");
        _image = grade.GetComponent<Image>();
        OrganManager.OnTransplantSuccessful += OnTransplantSuccessful;
    }

    private void OnTransplantSuccessful(char grade)
    {
        _image.sprite = grades.Find(g => g.name == grade.ToString());
        uiGameObject.SetActive(true);
    }
}
