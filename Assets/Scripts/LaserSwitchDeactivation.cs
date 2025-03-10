using System;
using UnityEngine;

public class LaserSwitchDeactivation : MonoBehaviour
{
    public GameObject laser;
    public Material unlockedMat;
    public GameObject player;

    void LaserDeactivation()
    {
        laser.SetActive(false);
        Renderer screen = transform.Find("prop_switchUnit_screen").GetComponent<Renderer>();
        screen.material = unlockedMat;
        GetComponent<AudioSource>().Play();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            if (Input.GetButton("Switch"))
            {
                LaserDeactivation();
            }
        }
    }
}