using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColliderPosition : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colided");

        if (collision.gameObject.tag == "TileWather")
        {
            Debug.Log("Do something else here");
        }

        if (collision.gameObject.tag == "TileGround")
        {
            Debug.Log("Do something else here");
        }
    }
}
