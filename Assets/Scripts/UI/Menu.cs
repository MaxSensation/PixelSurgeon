using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button startButton = default;
    [SerializeField] private string sceneToLoad = default;
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
