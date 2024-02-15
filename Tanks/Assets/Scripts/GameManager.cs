using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image dimScreen;
    [SerializeField] private Image pauseMenu;
    public static bool paused = false;

    void Awake()
    {
        pauseMenu.enabled = false;
        dimScreen.enabled = false;
    }
    void Update()
    {
        Restart();
        Pause();
    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            dimScreen.enabled = true;
            pauseMenu.enabled = true;
            Time.timeScale = 0f;
            paused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            Time.timeScale = 1f;
            dimScreen.enabled = false;
            pauseMenu.enabled = false;
            paused = false;
        }
    }
}
