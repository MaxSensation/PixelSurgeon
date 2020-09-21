using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Grade : MonoBehaviour
{
    [SerializeField] private List<Sprite> grades;
    [SerializeField] private GameObject grade, uiGameObject;
    [SerializeField] private AudioClip win, lose;
    private AudioSource _audioSource;
    private Image _image;
    private void Start()
    {
        _image = grade.GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
        OrganManager.OnTransplantSuccessfulEvent += UpdateGrade;
        OrganManager.OnLostToMuchBloodEvent += () => UpdateGrade('F');
    }

    private void OnDestroy()
    {
        OrganManager.OnTransplantSuccessfulEvent = null;
        OrganManager.OnLostToMuchBloodEvent = null;
    }

    private void UpdateGrade(char gradeChar)
    {
        uiGameObject.SetActive(true);
        _image.sprite = grades.Find(g => g.name == gradeChar.ToString());
        _audioSource.clip = gradeChar == 'F' ? lose : win;
        _audioSource.Play();
    }
}
