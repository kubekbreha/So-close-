using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColliderPosition : MonoBehaviour
{
    
    public GameObject player;
    public Button jumpButton;

    private GameObject collidingTile;

    private string collidingWith;

    private float myCounter;
    private float myLimit = 1.0f;


    // Start is called before the first frame update
    void Start()
    {

     
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        myCounter += Time.deltaTime; 
        if (myCounter >= myLimit)
        {
            ColorBlock colors = jumpButton.colors;
            colors.normalColor = Color.white;
            colors.pressedColor = Color.white;
            colors.highlightedColor = Color.white;
            colors.disabledColor = Color.white;
            jumpButton.colors = colors;
        }
        else
        {
            ColorBlock colors = jumpButton.colors;
            colors.normalColor = Color.gray;
            colors.pressedColor = Color.gray;
            colors.highlightedColor = Color.gray;
            colors.disabledColor = Color.gray;
            jumpButton.colors = colors;
        }
    }


    void OnTriggerEnter(Collider colider)
    {
        if (colider.gameObject.tag == "TileWather")
        {
            collidingTile = colider.gameObject;
            collidingWith = colider.gameObject.tag;
        }

        if (colider.gameObject.tag == "TileGround")
        {
            collidingTile = colider.gameObject;
            collidingWith = colider.gameObject.tag;
        }
    }



    public void JumpTile()
    {
        if (myCounter >= myLimit) {
            player.transform.position = collidingTile.transform.position + new Vector3(0, 0.3F, 0);
            player.transform.position = new Vector3(player.transform.position.x, 0.8F, player.transform.position.z);
            myCounter = 0f;
        }
    }

}

