using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockButton : MonoBehaviour
{
    [SerializeField] private Sprite unpressed, pressed;
    public static bool buttonPressed = false;
    private GameObject player;
    private GameObject pushBlock;
    private int triggers = 0;


    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        pushBlock = GameObject.FindWithTag("PushBlock");
    }

    void Update()
    {
        Debug.Log(triggers);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player || other.gameObject == pushBlock)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = pressed;
            triggers++;
            buttonPressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player || other.gameObject == pushBlock)
        {
            if (triggers == 1)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = unpressed;
                buttonPressed = false;
            }

            triggers--;
        }
    }
}
