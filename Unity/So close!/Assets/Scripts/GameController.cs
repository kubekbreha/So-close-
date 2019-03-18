using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    
    public GameObject hexPrefab;
    public GameObject playerPrefab;

    GameObject player;
    int speed = 20;


    // Size of the map in terms of number of hex tiles
    // This is NOT representative of the amount of 
    // world space that we're going to take up.
    // (i.e. our tiles might be more or less than 1 Unity World Unit)
    int width = 20;
    int height = 20;
    Vector3[,] tilesPossitions = new Vector3[20, 20];
    int actualTileX;
    int actualTileY;

    float xOffset = 0.882f;
    float zOffset = 0.764f;

    // Use this for initialization
    protected void Start()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                float xPos = x * xOffset;

                // Are we on an odd row?
                if (y % 2 == 1)
                {
                    xPos += xOffset / 2f;
                }

                Vector3 possition = new Vector3(xPos, 0, y * zOffset);
                GameObject hex_go = (GameObject)Instantiate(hexPrefab, possition, Quaternion.identity);

                Vector3 possition2 = new Vector3(xPos, 0.3F, y * zOffset);

                tilesPossitions[x, y] = possition2;

                // Name the gameobject something sensible.
                hex_go.name = "Hex_" + x + "_" + y;

                // For a cleaner hierachy, parent this hex to the map
                hex_go.transform.SetParent(this.transform);
                hex_go.transform.Rotate(90, 0, 0);

                //maybe need to change
                hex_go.isStatic = true;

            }
        }

        actualTileX = 10;
        actualTileY = 10;
        player = (GameObject)Instantiate(playerPrefab, tilesPossitions[actualTileX, actualTileY], Quaternion.identity);
       
    }
	
	// Update is called once per frame
	void Update () {
        //move around D button which is center
        if (Input.GetButtonDown("E"))
        {
            this.movePlayer("E");
        }
        if (Input.GetButtonDown("R"))
        {
            this.movePlayer("R");
        }
        if (Input.GetButtonDown("F"))
        {
            this.movePlayer("F");
        }
        if (Input.GetButtonDown("C"))
        {
            this.movePlayer("C");
        }
        if (Input.GetButtonDown("X"))
        {
            this.movePlayer("X");
        }
        if (Input.GetButtonDown("S"))
        {
            this.movePlayer("S");
        }
	}

    private void movePlayer(String direction){
        float step = speed * Time.deltaTime;

        switch (direction)
        {
            case "E":
                actualTileX++;
                actualTileY++;
                if(actualTileX > width){
                    actualTileX = 20; 
                }
                if (actualTileY > height)
                {
                    actualTileY = 20;
                }
                Debug.Log(tilesPossitions[actualTileX, actualTileY]);
                transform.position = Vector3.MoveTowards(transform.position, tilesPossitions[actualTileX, actualTileY], step);
                break;
          
            case "R":
                actualTileX--;
                actualTileY++;
                if (actualTileX < 0)
                {
                    actualTileX = 0;
                }
                if (actualTileY > height)
                {
                    actualTileY = 20;
                }
                Debug.Log(tilesPossitions[actualTileX, actualTileY]);
                transform.position = Vector3.MoveTowards(transform.position, tilesPossitions[actualTileX, actualTileY], step);
                break;

            case "F":
                actualTileX++;
                actualTileY--;
                if (actualTileX > width)
                {
                    actualTileX = 20;
                }
                if (actualTileY < height)
                {
                    actualTileY = 0;
                }
                Debug.Log(tilesPossitions[actualTileX, actualTileY]);
                transform.position = Vector3.MoveTowards(transform.position, tilesPossitions[actualTileX, actualTileY], step);
                break;

            case "C":
                actualTileX--;
                actualTileY--;
                if (actualTileX < 0)
                {
                    actualTileX = 0;
                }
                if (actualTileY < 0)
                {
                    actualTileY = 0;
                }
                Debug.Log(tilesPossitions[actualTileX, actualTileY]);
                transform.position = Vector3.MoveTowards(transform.position, tilesPossitions[actualTileX, actualTileY], step);
                break;

            case "X":
                //actualTileX++;
                //actualTileY++;
                //transform.position = Vector3.MoveTowards(transform.position, tilesPossitions[actualTileX, actualTileY], step); 
                break;

            case "S":
                //actualTileX++;
                //actualTileY++;
                //transform.position = Vector3.MoveTowards(transform.position, tilesPossitions[actualTileX, actualTileY], step);  
                break;
        }



    }
}
