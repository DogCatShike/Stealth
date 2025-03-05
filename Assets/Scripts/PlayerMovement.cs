using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip shoutingClip;
    public float turnSmoothing = 15f;
    public float speedDampTime = 0.1f;

    Animator animator;
    private HashIDs hash;

    void Awake()
    {
        animator = GetComponent<Animator>();
        hash = GetComponent<HashIDs>();
        animator.SetLayerWeight(1, 1);
    }

    void FixedUpdate()
    {
        float dt = Time.deltaTime;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool sneaking = Input.GetButton("Sneak");

        MovementManagement(h, v, sneaking, dt);
    }

    void Update()
    {
        bool shout = Input.GetButtonDown("Attract");
        animator.SetBool(hash.shoutingBool, shout);
        AudioManagement(shout);
    }

    void Rotating(float h, float v, float dt)
    {
        Vector3 targetDir = new Vector3(h, 0, v);
        Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
        Rigidbody rb = GetComponent<Rigidbody>();
        Quaternion newRotation = Quaternion.Lerp(rb.rotation, targetRotation, turnSmoothing * dt);
        rb.MoveRotation(newRotation);
    }

    void MovementManagement(float h, float v, bool sneaking, float dt)
    {
        animator.SetBool(hash.sneakingBool, sneaking);

        if (h != 0 || v != 0)
        {
            Rotating(h, v, dt);
            animator.SetFloat(hash.speedFloat, 5.5f, speedDampTime, dt);
        }
        else
        {
            animator.SetFloat(hash.speedFloat, 0);
        }
    }

    void AudioManagement(bool shout)
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash == hash.locomotionState)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        if (shout)
        {
            AudioSource.PlayClipAtPoint(shoutingClip, transform.position);
        }
    }
}