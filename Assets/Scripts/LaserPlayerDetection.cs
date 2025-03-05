using System;
using UnityEngine;

public class LaserPlayerDetection : MonoBehaviour
{
    GameObject player;
    LastPlayerSighting lastPlayerSighting;

    void Start()
    {
        player = GameObject.FindWithTag(Tags.Player);
        lastPlayerSighting = GameObject.FindWithTag(Tags.GameController).GetComponent<LastPlayerSighting>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Renderer>().enabled)
        {
            if (other.gameObject == player)
            {
                lastPlayerSighting.position = other.transform.position;
            }
        }
    }
}