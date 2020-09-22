using System.Collections.Generic;
using Human;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
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
            BodyPartManager.OnWinEvent += UpdateGrade;
            BodyPartManager.OnGameOverEvent += () => UpdateGrade('F');
        }

        private void OnDestroy()
        {
            BodyPartManager.OnWinEvent -= UpdateGrade;
            BodyPartManager.OnGameOverEvent = null;
        }

        private void UpdateGrade(char gradeChar)
        {
            uiGameObject.SetActive(true);
            _image.sprite = grades.Find(g => g.name == gradeChar.ToString());
            _audioSource.clip = gradeChar == 'F' ? lose : win;
            _audioSource.Play();
        }
    }
}