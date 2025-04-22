using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
    public float patrolSpeed = 2;
    public float chaseSpeed = 5;
    public float chaseWaitTime = 5;
    public float patrolWaitTime = 1;
    public Transform[] patrolWaypoints;

    EnemySight enemySight;
    NavMeshAgent nav;
    Transform player;
    PlayerHealth playerHealth;
    LastPlayerSighting lastPlayerSighting;
    float chaseTimer;
    float patrolTimer;
    int wayPointIndex;

    void Awake() {
        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<LastPlayerSighting>();
    }

    void Update() {
        float dt = Time.deltaTime;

        if (enemySight.playerInSight && playerHealth.health > 0) {
            Shooting();
        } else if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition && playerHealth.health > 0) {
            Chasing(dt);
        } else {
            Patrolling(dt);
        }
    }

    void Shooting() {
        nav.isStopped = true;
    }

    void Chasing(float dt) {
        Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;
        if (sightingDeltaPos.sqrMagnitude > 4) {
            nav.destination = enemySight.personalLastSighting;
        }
        nav.speed = chaseSpeed;

        if (nav.remainingDistance < nav.stoppingDistance) {
            chaseTimer += dt;

            if (chaseTimer >= chaseWaitTime) {
                lastPlayerSighting.position = lastPlayerSighting.resetPosition;
                enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
                chaseTimer = 0;
            }
        } else {
            chaseTimer = 0;
        }
    }

    void Patrolling(float dt) {
        nav.speed = patrolSpeed;

        if (nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance) {
            patrolTimer += dt;

            if (patrolTimer >= patrolWaitTime) {
                if (wayPointIndex == patrolWaypoints.Length - 1) {
                    wayPointIndex = 0;
                } else {
                    wayPointIndex++;
                }
            }
        } else {
            patrolTimer = 0;
        }

        nav.destination = patrolWaypoints[wayPointIndex].position;
    }
}