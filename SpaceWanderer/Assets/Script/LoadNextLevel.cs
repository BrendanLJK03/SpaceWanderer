using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    bool isTouch;
    public Animator anim;
    public int levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            if(isTouch == true)
            {
                FadeToLevel(levelToLoad);
            }
        }
    }

    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        anim.SetTrigger("FadeOut");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isTouch = true;
        }
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}