using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private string sceneToLoad;

        private void Start()
        {
            startButton.onClick.AddListener(LoadGame);
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(LoadGame);
        }

        private void LoadGame()
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}