using System;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public AudioClip keyGrab;
    GameObject player;
    PlayerInventory playerInventory;

    void Awake()
    {
        player = GameObject.FindWithTag(Tags.Player);
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            AudioSource.PlayClipAtPoint(keyGrab, transform.position);
            playerInventory.hasKey = true;
            Destroy(gameObject);
        }
    }
}