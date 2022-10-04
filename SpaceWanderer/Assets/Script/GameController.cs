using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int meteorite;
    public bool destroyedAll = false;
    GameController gc;
    public Text metoerLeftText;
    // Start is called before the first frame update
    private void Awake()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        UpdateMeteorLeftText();
    }


    // Update is called once per frame
    void Update()
    {
        BlockNPC n = gameObject.GetComponent<BlockNPC>();

        if(meteorite <= 0)
        {
            destroyedAll = true;
            n. MissionComplete();
        }
        else
        {
            destroyedAll = false;
        }
    }

    public void UpdateMeteorLeftText()
    {
        metoerLeftText.text = $"Meteor Left : {meteorite}";
    }
}
