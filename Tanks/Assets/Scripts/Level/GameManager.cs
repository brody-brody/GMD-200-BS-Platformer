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
    private const int TITLE_SCREEN = 0;

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
        // if the game is started and you press r, restart
        if (Input.GetKeyDown(KeyCode.R) && Clicking.started)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        // if the game is paused and you press r
        if (Input.GetKeyDown(KeyCode.Q) && paused)
        {
            // reset time scale and static variables, and load title screen
            Time.timeScale = 1f;
            Clicking.started = false;
            GameManager.paused = false;
            Timer._time = 0f;
            SceneManager.LoadScene(TITLE_SCREEN);
        }
    }

    void Pause()
    {
        // pause game when hitting escape by setting time scale to 0 and bringing up pause screen
        if (Input.GetKeyDown(KeyCode.Escape) && !paused && Clicking.started)
        {
            dimScreen.enabled = true;
            pauseMenu.enabled = true;
            Time.timeScale = 0f;
            paused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused && Clicking.started)
        {
            Time.timeScale = 1f;
            dimScreen.enabled = false;
            pauseMenu.enabled = false;
            paused = false;
        }
    }
}
