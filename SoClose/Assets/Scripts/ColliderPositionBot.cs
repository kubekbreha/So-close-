using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColliderPositionBot : MonoBehaviour
{
    
    public GameObject player;
    public GameObject target;
    public GameObject gameOverCanvas;

    private GameObject collidingTile;


    private string collidingWith;
    private float myCounter;
    private float myLimit = 1.5f;


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
        player.transform.LookAt(target.transform);

        myCounter += Time.deltaTime;

        float dist = Vector3.Distance(player.transform.position, target.transform.position);

        if(dist>1 ){
            JumpTile();
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


        if (myCounter >= myLimit && collidingTile!= null) {
            player.transform.position = collidingTile.transform.position + new Vector3(0, 0.3F, 0);
            player.transform.position = new Vector3(player.transform.position.x, 0.8F, player.transform.position.z);
            if (collidingTile.transform.position.y < -10f){
                    player.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    gameOverCanvas.SetActive(true);
            }
           
            myCounter = 0f;
        }else  if(collidingTile == null){
            player.transform.LookAt(target.transform);
        }
    }

}

