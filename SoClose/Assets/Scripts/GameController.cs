﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using uPLibrary.Networking.M2Mqtt;
//using uPLibrary.Networking.M2Mqtt.Messages;

public class GameController : MonoBehaviour {

    public float speed;
    public FixedJoystick fixedJoystickLeft;
    public FixedJoystick fixedJoystickRight;

    public GameObject hexPrefab;
    public GameObject hexWaterPrefab;
    public GameObject player;

    //int[,] board = new int[,] { 
    //    {  9, -2, -1, 0, 1, 2, 9 }, 
    //    {  9, -2, -1, 0, 1, 2, 9 }, 
    //    { -3, -2, -1, 0, 1, 2, 9}, 
    //    { -3, -2, -1, 0, 1, 2, 3 }, 
    //    {  9, -3, -2, -1, 0, 1, 2}, 
    //    {  9, -2, -1, 0, 1, 2, 9}, 
    //    { 9, 9, -2, -1, 0, 1, 9} 
    //};

    int[,] board = new int[,] {
        {  0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {  0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {  0,0,0,0,0,1,1,1,0,0,0,0,0,0 },
        {  0,0,0,0,1,1,1,1,1,1,0,0,0,0 },
        {  0,0,0,1,1,1,1,1,1,1,1,0,0,0 },
        {  0,0,1,1,1,1,1,1,1,1,1,1,0,0 },
        {  0,0,1,1,1,1,1,1,1,1,1,1,0,0 },
        {  0,0,1,1,1,1,1,1,1,1,1,1,0,0 },
        {  0,0,1,1,1,1,1,1,1,1,1,1,0,0 },
        {  0,0,0,1,1,1,1,1,1,1,1,0,0,0 },
        {  0,0,0,0,1,1,1,1,1,1,0,0,0,0 },
        {  0,0,0,0,0,0,1,1,1,0,0,0,0,0 },
        {  0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        {  0,0,0,0,0,0,0,0,0,0,0,0,0,0 }

    };


    // Size of the map in terms of number of hex tiles
    // This is NOT representative of the amount of 
    // world space that we're going to take up.
    // (i.e. our tiles might be more or less than 1 Unity World Unit)
    int width = 14;
    int height = 14;
    Vector3[,] tilesPossitions = new Vector3[14, 14];
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

                if (board[x, y] == 0)
                {
                    Vector3 possition = new Vector3(xPos, 0, y * zOffset);
                    GameObject hex_go = (GameObject)Instantiate(hexWaterPrefab, possition, Quaternion.identity);

                    Vector3 possition2 = new Vector3(xPos, 0.3F, y * zOffset);

                    tilesPossitions[x, y] = possition2;

                    // Name the gameobject something sensible.
                    hex_go.name = "Wat_" + x + "_" + y;

                    // For a cleaner hierachy, parent this hex to the map
                    hex_go.transform.SetParent(this.transform);
                    //hex_go.transform.Rotate(90, 0, 0);

                    //maybe need to change
                    hex_go.isStatic = true;
                }else{
                    Vector3 possition = new Vector3(xPos, 0, y * zOffset);
                    GameObject hex_go = (GameObject)Instantiate(hexPrefab, possition, Quaternion.identity);

                    Vector3 possition2 = new Vector3(xPos, 0.3F, y * zOffset);

                    tilesPossitions[x, y] = possition2;

                    // Name the gameobject something sensible.
                    hex_go.name = "Hex_" + x + "_" + y;

                    // For a cleaner hierachy, parent this hex to the map
                    hex_go.transform.SetParent(this.transform);
                    //hex_go.transform.Rotate(90, 0, 0);

                    //maybe need to change
                    hex_go.isStatic = true;
                }
            }
        }

        actualTileX = 9;
        actualTileY = 5;
        player.transform.position = tilesPossitions[actualTileX, actualTileY];

    }

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * fixedJoystickLeft.Vertical + Vector3.right * fixedJoystickLeft.Horizontal;

        Vector3 rotation = Vector3.forward * fixedJoystickRight.Vertical + Vector3.right * fixedJoystickRight.Horizontal;
        Twist(fixedJoystickRight.Horizontal, fixedJoystickRight.Vertical);

        //float angle = (float)(fixedJoystickRight.Vertical * (180.0 / Math.PI));
        //rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }


    private void Twist(float h1, float v1)
    {
        if (h1 == 0f && v1 == 0f)
        { // this statement allows it to recenter once the inputs are at zero 
            Vector3 curRot = player.transform.localEulerAngles; // the object you are rotating
            Vector3 homeRot;
            if (curRot.y > 180f)
            { // this section determines the direction it returns home 
                Debug.Log(curRot.y);
                homeRot = new Vector3(0f, 359.999f, 0f); //it doesnt return to perfect zero 
            }
            else
            {                                                                      // otherwise it rotates wrong direction 
                homeRot = Vector3.zero;
            }
            player.transform.localEulerAngles = Vector3.Slerp(curRot, homeRot, Time.deltaTime * 2);
        }
        else
        {
            player.transform.localEulerAngles = new Vector3(0f, Mathf.Atan2(h1, v1) * 180 / Mathf.PI, 0f); // this does the actual rotaion according to inputs
        }
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
      
	}

    private void movePlayer(String direction){
        switch (direction)
        {
            case "E":
                actualTileX++;
                actualTileY++;
                if (actualTileX >= width)
                {
                    actualTileX = 9;
                }
                if (actualTileY >= height)
                {
                    actualTileY = 9;
                }
                Debug.Log(tilesPossitions[actualTileX, actualTileY]);
                player.transform.position = tilesPossitions[actualTileX, actualTileY];
                break;

            case "R":
                actualTileX--;
                actualTileY++;
                if (actualTileX < 0)
                {
                    actualTileX = 0;
                }
                if (actualTileY >= height)
                {
                    actualTileY = 9;
                }
                Debug.Log(tilesPossitions[actualTileX, actualTileY]);
                player.transform.position = tilesPossitions[actualTileX, actualTileY];
                break;

            case "F":
                actualTileX++;
                actualTileY--;
                if (actualTileX >= width)
                {
                    actualTileX = 9;
                }
                if (actualTileY < height)
                {
                    actualTileY = 0;
                }

                Debug.Log(actualTileX);
                Debug.Log(actualTileY);
                Debug.Log(tilesPossitions[actualTileX, actualTileY]);

                player.transform.position = tilesPossitions[actualTileX, actualTileY];
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
                player.transform.position = tilesPossitions[actualTileX, actualTileY];
                break;

        }
    }
}
