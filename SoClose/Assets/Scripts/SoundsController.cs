using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsController : MonoBehaviour
{
    public static SoundsController Instance { get; private set; }

    public AudioSource ahoj;

    public AudioSource zasah;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayAhoj()
    {
        ahoj.Play();
    }
    public void PlayZasah()
    {
        zasah.Play();
    }
}
