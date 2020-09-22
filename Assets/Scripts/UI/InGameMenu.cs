using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private Button next, menu;
        [SerializeField] private string sceneToLoad;
        private void Start()
        {
            menu.onClick.AddListener(Menu);
            next.onClick.AddListener(LoadNextLevel);
        }

        private void OnDestroy()
        {
            menu.onClick.RemoveListener(Menu);
            next.onClick.RemoveListener(LoadNextLevel);
        }

        private void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void Menu()
        {
            GameManager.DeleteThis();
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}