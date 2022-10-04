using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    
    public static event Action<Meteor> OnMeteorDestroy;
    bool isTouch;
    public GameObject inteButton;
    GameController gc;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gc.meteorite++;
    }

    // Update is called once per frame
    void Update()
    {
        gc.UpdateMeteorLeftText();
        if(isTouch )
        {
            inteButton.SetActive(true);
            if(Input.GetKeyDown(KeyCode.X) && inteButton.activeInHierarchy)
            {
                Destroy(gameObject);
                inteButton.SetActive(false);
                gc.meteorite--;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isTouch = false;
        }
    }
}