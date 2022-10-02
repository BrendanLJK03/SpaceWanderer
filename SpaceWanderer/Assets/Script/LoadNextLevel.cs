using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevel : MonoBehaviour
{
    public string LevelName;  //this is to insert level
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if(other.tag == "Player")
        {
            Application.LoadLevel(LevelName); //this is to load the level
        }
    }
}
