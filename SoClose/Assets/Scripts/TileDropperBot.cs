using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDropperBot : MonoBehaviour
{
    private Collider collidingTile;
    private string collidingWith;
    public Rigidbody tileGround;
    public GameObject playerAnimal;

    public GameObject gameOverCanvas;


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider colider)
    {


        if (colider.gameObject.tag == "TileWather")
        {
            Debug.Log("TILEWATHER");
            if (collidingTile != null)
            {
                Rigidbody clone;
                clone = Instantiate(tileGround, collidingTile.transform.position, collidingTile.transform.rotation);
                clone.useGravity = true;
                collidingTile.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }

            //GAMEOVER handeling
            GetComponent<Collider>().attachedRigidbody.useGravity = true;
            playerAnimal.gameObject.GetComponent<Rigidbody>().useGravity = true;

            gameOverCanvas.SetActive(true);
        }else if (colider.gameObject.tag == "TileGround")
        {
            Debug.Log("TileGround");

            if(colider.gameObject.GetComponent<MeshRenderer>().enabled == false){
                GetComponent<Collider>().attachedRigidbody.useGravity = true;
                playerAnimal.gameObject.GetComponent<Rigidbody>().useGravity = true;
            }

            if (collidingTile != null){
                Rigidbody clone;
                clone = Instantiate(tileGround, collidingTile.transform.position, collidingTile.transform.rotation);
                clone.useGravity = true;
                collidingTile.gameObject.GetComponent<MeshRenderer>().enabled = false;   
            }

            collidingTile = colider;
            collidingWith = colider.gameObject.tag;
        }else{
            Debug.Log("Enemy");
            gameOverCanvas.SetActive(true);
        }
    }

}
