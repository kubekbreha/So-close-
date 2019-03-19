using System;
using UnityEngine;

public class Controller
{
    public string PlayerId { get; private set; }

    public string PlayerName { get; set; }

    public int Score { get; set; }

    public GameObject GameObject { get; set; }

    public long LastChange { get; private set; }

    public Vector3 DirectionVector { get; set; }

    public int Fire { get; set; }

    public int PrefabIndex { get; set; }

    public Controller(string playerId, string playerName)
    {
        PlayerId = playerId;
        PlayerName = playerName;
        Alive();
    }

    public void Alive()
    {
        LastChange = DateTime.Now.Ticks;
    }
}
