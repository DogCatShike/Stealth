using System;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float maximumDamage = 120;
    public float minimumDamage = 45;
    public AudioClip shotClip;
    public float flashInstensity = 3;
    public float fadeSpeed = 10;
    Animator anim;
    HashIDs hash;
    LineRenderer laserShotLine;
    Light laserShotLight;
    SphereCollider col;
    Transform player;
    PlayerHealth playerHealth;
    bool shooting;
    float scaledDamage;

    void Awake()
    {
        anim = GetComponent<Animator>();
        laserShotLine = GetComponentInChildren<LineRenderer>();
        laserShotLight = laserShotLine.gameObject.GetComponentInParent<Light>();
        col = GetComponent<SphereCollider>();
        player = GameObject.FindWithTag(Tags.Player).transform;
        playerHealth = player.gameObject.GetComponent<PlayerHealth>();
        hash = GameObject.FindWithTag(Tags.Player).GetComponent<HashIDs>();
        laserShotLine.enabled = false;
        laserShotLight.intensity = 0;
        scaledDamage = maximumDamage - minimumDamage;
    }

    void Update()
    {
        float dt = Time.deltaTime;

        float shot = anim.GetFloat(hash.shotFloat);
        if (shot > 0.5f && !shooting)
        {
            Shoot();
        }
        if (shot < 0.5f)
        {
            shooting = false;
            laserShotLine.enabled = false;
        }
        laserShotLight.intensity = Mathf.Lerp(laserShotLight.intensity, 0, fadeSpeed * dt);
    }

    void OnAnimatorIK(int layerIndex)
    {
        float AimWeight = anim.GetFloat(hash.aimWeightFloat);
        anim.SetIKPosition(AvatarIKGoal.RightHand, player.position + Vector3.up * 1.5f);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, AimWeight);
    }

    void ShotEffects()
    {
        laserShotLine.SetPosition(0, laserShotLine.transform.position);
        laserShotLine.SetPosition(1, player.position + Vector3.up * 1.5f);
        laserShotLine.enabled = true;
        laserShotLight.intensity = flashInstensity;
        AudioSource.PlayClipAtPoint(shotClip, laserShotLight.transform.position);
    }

    void Shoot()
    {
        shooting = true;
        float fractionalDistance = (col.radius - Vector3.Distance(transform.position, player.position)) / col.radius;
        float damage = scaledDamage * fractionalDistance + minimumDamage;
        playerHealth.TakeDamage(damage);
        ShotEffects();
    }
}