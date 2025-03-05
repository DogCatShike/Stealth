using System;
using UnityEngine;

public class CCTVPlayerDetection : MonoBehaviour
{
    GameObject player;
    LastPlayerSighting lastPlayerSighting;

    void Start()
    {
        player = GameObject.FindWithTag(Tags.Player);
        lastPlayerSighting = GameObject.FindWithTag(Tags.GameController).GetComponent<LastPlayerSighting>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Vector3 relPlayerPos = player.transform.position - transform.position;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, relPlayerPos, out hit))
            {
                if (hit.collider.gameObject == player)
                {
                    lastPlayerSighting.position = player.transform.position;
                }
            }
        }
    }
}
