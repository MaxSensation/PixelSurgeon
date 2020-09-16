using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button startButton = default, quit = default;
    [SerializeField] private string sceneToLoad = default;
    private void Start()
    {
        startButton.onClick.AddListener(LoadGame);
        quit.onClick.AddListener(QuitGame);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    
    private void QuitGame()
    {
        Application.Quit();
    }
}
