using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button startButton, quit;
    [SerializeField] private string sceneToLoad;
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
