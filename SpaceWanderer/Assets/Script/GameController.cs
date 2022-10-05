using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int meteorite;
    public bool destroyedAll = false;
    GameController gc;
    public Text meteorLeftText;

    public GameOverScene GameOverScene;
    public GameCompleteScene GameCompleteScene;

    // Start is called before the first frame update
    private void Awake()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        UpdateMeteorLeftText();
    }


    // Update is called once per frame
    void Update()
    {
        BlockNPC n = GetComponent<BlockNPC>();

        if(meteorite <= 0)
        {
            destroyedAll = true;
            Destroy (GameObject.FindWithTag("BlockNPC"));
        }
        else
        {
            destroyedAll = false;
        }
    }

    public void UpdateMeteorLeftText()
    {
        meteorLeftText.text = $"Meteor Left : {meteorite}";

        if(meteorite <= 0)
        {
            meteorLeftText.text = $"All meteor destroyed";
        }
    }

    public void GameOver()
    {
        GameOverScene.Setup();
    }

    public void GameComplete()
    {
        GameCompleteScene.MissionComplete();
    }

    
}
