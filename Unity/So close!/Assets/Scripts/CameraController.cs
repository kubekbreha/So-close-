using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    public GameObject player;       

    private Vector3 offset;       



    void Start()
    {
        offset = player.transform.position + new Vector3(0,5,-4);
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
