using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    public static float _time = 0f;
    public static string displayTime;

    void Awake()
    {
        timeText.text = displayTime;
    }

    void Update()
    {
        RunTimer();

        if (Input.GetKeyDown(KeyCode.U))
        {
            Clicking.started = !Clicking.started;
        }
    }

    void RunTimer()
    {
        if (Clicking.started)
        {
            _time += 1f * Time.deltaTime;

            TimeSpan currentTime = TimeSpan.FromSeconds(_time);

            displayTime = currentTime.Minutes.ToString() + ":" + currentTime.Seconds.ToString() + "." + (currentTime.Milliseconds / 10).ToString();

            timeText.text = displayTime;
        }
    }
}
