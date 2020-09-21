
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMeny : MonoBehaviour
{
    [SerializeField] private Button next = default, menu = default;
    [SerializeField] private string sceneToLoad = default;
    
    void Start()
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
        SceneManager.LoadScene(sceneToLoad);
    }
}
