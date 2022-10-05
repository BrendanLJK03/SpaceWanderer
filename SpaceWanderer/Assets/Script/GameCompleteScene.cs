using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCompleteScene : MonoBehaviour
{
    public void MissionComplete()
    {
        gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void ExtiButton()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }
}
