using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    [SerializeField] private Image dimScreen;
    [SerializeField] private Image pauseMenu;
    [SerializeField] private Sprite finished;
    [SerializeField] private Sprite finalFinished;
    private int current;
    private const int TITLE_SCREEN = 0;
    private const int FIRST_LEVEL = 1;
    private const int FINAL_LEVEL = 5;
    public static bool gameEnd = false;

    void Update()
    {
        NextLevel();

        current = SceneManager.GetActiveScene().buildIndex;

        // if the game has been ended
        if (gameEnd)
        {
            // quit to main menu and reset variables
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Time.timeScale = 1f;
                Clicking.started = false;
                GameManager.paused = false;
                gameEnd = false;
                Timer._time = 0f;

                SceneManager.LoadScene(TITLE_SCREEN);
            }
            // restart to level 1 and reset variables
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Time.timeScale = 1f;
                Clicking.started = false;
                GameManager.paused = false;
                gameEnd = false;
                Timer._time = 0f;
                Clicking.started = true;

                SceneManager.LoadScene(FIRST_LEVEL);
            }
        }
    }

    // when player enters finish tower trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // pause game, bring up menu, and switch the sprite to the finish sprite
            Clicking.started = false;
            GameManager.paused = true;
            Time.timeScale = 0f;

            dimScreen.enabled = true;
            pauseMenu.enabled = true;
            pauseMenu.sprite = finished;

            // if the current level is the final level, show a different sprite
            if (current == FINAL_LEVEL)
            {
                gameEnd = true;
                pauseMenu.sprite = finalFinished;
            }
        }
    }

    // if youve reached the end of the level and hit enter, go to next level
    void NextLevel()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !Clicking.started && GameManager.paused)
        {
            Time.timeScale = 1f;
            Clicking.started = true;
            GameManager.paused = false;

            // go to next level as long as it is not the final level
            if (current < FINAL_LEVEL)
                SceneManager.LoadScene(current + 1);
        }
    }
}
