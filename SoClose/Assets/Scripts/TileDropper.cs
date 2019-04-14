using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDropper : MonoBehaviour
{
    private Collider collidingTile;
    private string collidingWith;
    public Rigidbody tileGround;
    public GameObject playerAnimal;

    public GameObject gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider colider)
    {
        
        if (colider.gameObject.tag == "TileWather")
        {
            if (collidingTile != null){
                Rigidbody clone;
                clone = Instantiate(tileGround, collidingTile.transform.position, collidingTile.transform.rotation);
                clone.useGravity = true;
                collidingTile.gameObject.GetComponent<MeshRenderer>().enabled = false;        
            }

            //GAMEOVER handeling
            GetComponent<Collider>().attachedRigidbody.useGravity = true;
            playerAnimal.gameObject.GetComponent<Rigidbody>().useGravity = true;

            gameOverCanvas.SetActive(true);
        }


        if (colider.gameObject.tag == "TileGround")
        {
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
        }
    }

}
