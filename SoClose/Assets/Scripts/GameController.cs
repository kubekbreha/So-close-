using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using uPLibrary.Networking.M2Mqtt;
//using uPLibrary.Networking.M2Mqtt.Messages;

public class GameController : MonoBehaviour {
    
    public float speed;
    public FixedJoystick fixedJoystickRight;

    public Vector3 whereToJump;

    public GameObject hexPrefab;
    public GameObject hexWaterPrefab;
    public GameObject player;
    public GameObject playerColider;

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
        Vector3 rotation = Vector3.forward * fixedJoystickRight.Vertical + Vector3.right * fixedJoystickRight.Horizontal;
        Twist(player, fixedJoystickRight.Horizontal, fixedJoystickRight.Vertical);

        //float angle = (float)(fixedJoystickRight.Vertical * (180.0 / Math.PI));
        //rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    public void JumpTile()
    {
        Debug.Log("jump tile");
    }

    private void Twist(GameObject gameObject, float h1, float v1)
    {
        if (h1 == 0f && v1 == 0f)
        { // this statement allows it to recenter once the inputs are at zero 
            Vector3 curRot = player.transform.localEulerAngles; // the object you are rotating
            Vector3 homeRot;
            if (curRot.y > 180f)
            { // this section determines the direction it returns home 
                homeRot = new Vector3(0f, 359.999f, 0f); //it doesnt return to perfect zero 
            }
            else
            {                                                                      // otherwise it rotates wrong direction 
                homeRot = Vector3.zero;
            }
            //gameObject.transform.localEulerAngles = Vector3.Slerp(curRot, homeRot, Time.deltaTime * 2);
        }
        else
        {
            gameObject.transform.localEulerAngles = new Vector3(0f, Mathf.Atan2(h1, v1) * 180 / Mathf.PI, 0f); // this does the actual rotaion according to inputs
        }    
    }

}
