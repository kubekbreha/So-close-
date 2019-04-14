using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColliderPosition : MonoBehaviour
{

    public GameObject player;

    private GameObject collidingTile;

    private string collidingWith;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
        player.transform.position = collidingTile.transform.position + new Vector3(0, 0.3F, 0);
    }

}

