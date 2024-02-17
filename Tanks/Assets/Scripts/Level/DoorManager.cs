using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private GameObject door;

    void Update()
    {
        ButtonCheck();
    }

    void ButtonCheck()
    {
        if (LockButton.buttonPressed)
        {
            door.SetActive(false);
        }
        else
        {
            door.SetActive(true);
        }
    }
}
