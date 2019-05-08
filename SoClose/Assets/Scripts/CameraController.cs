using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    
    public GameObject player;       

    private Vector3 offset;       



    void Start()
    {
        offset = player.transform.position + new Vector3(-10,10,-12);
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
