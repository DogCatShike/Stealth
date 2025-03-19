using System;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    public bool requireKey;
    public AudioClip doorSwitchClip;
    public AudioClip accessDeniedClip;
    Animator anim;
    HashIDs hash;
    GameObject player;
    PlayerInventory playerInventory;
    int count;

    void Awake()
    {
        anim = GetComponent<Animator>();
        hash = GameObject.FindWithTag(Tags.Player).GetComponent<HashIDs>();
        player = GameObject.FindWithTag(Tags.Player);
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    void Update()
    {
        anim.SetBool(hash.openBool, count > 0);
        AudioSource audio = GetComponent<AudioSource>();
        if (anim.IsInTransition(0) && !audio.isPlaying)
        {
            audio.clip = doorSwitchClip;
            audio.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (other.gameObject == player)
        {
            if (requireKey)
            {
                if (playerInventory.hasKey)
                {
                    count++;
                }
                else
                {
                    audio.clip = accessDeniedClip;
                    audio.Play();
                }
            }
            else
            {
                count++;
            }
        }

        else if (other.gameObject.tag == Tags.Enemy)
        {
            if (other.gameObject.tag == Tags.Enemy)
            {
                count++;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player || (other.gameObject.tag == Tags.Enemy && other is CapsuleCollider))
        {
            count = Mathf.Max(0, count - 1);
        }
    }
}