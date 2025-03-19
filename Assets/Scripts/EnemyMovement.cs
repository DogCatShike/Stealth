using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float deadZone = 5;
    Transform player;
    EnemySight enemySight;
    NavMeshAgent nav;
    Animator anim;
    HashIDs hash;
    SimpleLocomotion locomotion;

    void Awake()
    {
        player = GameObject.FindWithTag(Tags.Player).transform;
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<HashIDs>();
        nav.updateRotation = false;
        locomotion = new SimpleLocomotion(anim, hash);
        anim.SetLayerWeight(1, 1);
        anim.SetLayerWeight(2, 1);
        deadZone *= Mathf.Deg2Rad;
    }

    void Update()
    {
        float dt = Time.deltaTime;
        NavAnimSetup(dt);
    }

    void OnAnimatorMove()
    {
        nav.velocity = anim.deltaPosition / Time.deltaTime;
        transform.rotation = anim.rootRotation;
    }

    float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
    {
        if (toVector == Vector3.zero)
        {
            return 0;
        }

        float angle = Vector3.Angle(fromVector, toVector);
        Vector3 normal = Vector3.Cross(fromVector, toVector);
        angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
        angle *= Mathf.Rad2Deg;
        return angle;
    }

    void NavAnimSetup(float dt)
    {
        float speed;
        float angle;
        if (enemySight.playerInSight)
        {
            speed = 0;
            angle = FindAngle(transform.forward, player.position - transform.position, transform.up);
        }
        else
        {
            speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
            angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);

            if (Mathf.Abs(angle) < deadZone)
            {
                transform.LookAt(transform.position + nav.desiredVelocity);
                angle = 0;
            }
            locomotion.Do(speed, angle, dt);
        }
    }
}