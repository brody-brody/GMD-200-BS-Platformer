using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Image blackBox;
    [SerializeField] private TextMeshProUGUI endDisplayText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI instructions;
    public static float _time = 0f;
    public static string displayTime;
    private bool runFinish = false;

    public float bestTime;

    // hide objects that shouldnt be shown yet
    void Awake()
    {
        timeText.text = displayTime;

        blackBox.enabled = false;
        endDisplayText.enabled = false;
        bestScoreText.enabled = false;
        instructions.enabled = false;

        // get the current player prefs value, or set it to a very large number if there isnt one
        bestTime = PlayerPrefs.GetFloat("BestTime", 9999);
    }

    void Update()
    {
        RunTimer();

        // if the game has ended, run FinishGame() once
        if (LevelFinish.gameEnd)
        {
            if (!runFinish)
            {
                FinishGame();
                runFinish = true;
            }
        }

    }

    void RunTimer()
    {
        // if the game is running
        if (Clicking.started)
        {
            _time += 1f * Time.deltaTime;

            TimeSpan currentTime = TimeSpan.FromSeconds(_time);

            // displayTime is formatted minutes:seconds.hundredths
            displayTime = currentTime.Minutes.ToString() + ":" + currentTime.Seconds.ToString() + "." + (currentTime.Milliseconds / 10).ToString();

            timeText.text = displayTime;
        }
    }

    // will only run after beating level 5
    void FinishGame()
    {
        // display relevant objects
        blackBox.enabled = true;
        endDisplayText.enabled = true;
        bestScoreText.enabled = true;
        instructions.enabled = true;
        endDisplayText.text = "You beat the game with a time of " + displayTime + ".";

        // running Highscore() will also record a new high score into player prefs
        string bestTimeString = Highscore();

        bestScoreText.text = "Your best time is " + bestTimeString + ".";
    }

    string Highscore()
    {
        string temp = "";
        // if your time was a new best time, set it, and display it
        if (_time < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", _time);
            PlayerPrefs.Save();

            temp = displayTime;
        }
        // if your time was not a new best, fetch the old best time and format it the same as displayTime is
        else
        {
            float holder = PlayerPrefs.GetFloat("BestTime");

            TimeSpan best = TimeSpan.FromSeconds(holder);

            temp = best.Minutes.ToString() + ":" + best.Seconds.ToString() + "." + (best.Milliseconds / 10).ToString();
        }

        return temp;
    }
}
