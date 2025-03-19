using System;
using UnityEngine;

public class SimpleLocomotion
{
    public float speedDampTime = 0.1f;
    public float angularSpeedDampTime = 0.7f;
    public float angleResponseTime = 0.6f;
    Animator anim;
    HashIDs hash;

    public SimpleLocomotion(Animator animator, HashIDs hashIDs)
    {
        anim = animator;
        hash = hashIDs;
    }

    public void Do(float speed, float angle, float dt)
    {
        float angularSpeed = angle / angleResponseTime;
        anim.SetFloat(hash.speedFloat, speed, speedDampTime, dt);
        anim.SetFloat(hash.angularSpeedFloat, angularSpeed, angularSpeedDampTime, dt);
    }
}