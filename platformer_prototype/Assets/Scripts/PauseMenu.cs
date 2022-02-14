using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    Scene _activeSceneName;

    private void Awake() {
        _activeSceneName = SceneManager.GetActiveScene();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
        Debug.Log(_activeSceneName);
    }
    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Reload() {
        SceneManager.LoadScene(_activeSceneName.name);
    }
    public void QuitGame() {
        Application.Quit();
    }
}
