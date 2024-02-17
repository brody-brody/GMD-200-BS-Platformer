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

    void Update()
    {
        NextLevel();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Clicking.started = false;
            GameManager.paused = true;
            Time.timeScale = 0f;

            dimScreen.enabled = true;
            pauseMenu.enabled = true;
            pauseMenu.sprite = finished;
        }
    }

    void NextLevel()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !Clicking.started && GameManager.paused)
        {
            int current = SceneManager.GetActiveScene().buildIndex;

            Time.timeScale = 1f;
            Clicking.started = true;
            GameManager.paused = false;

            SceneManager.LoadScene(current + 1);
        }
    }
}
