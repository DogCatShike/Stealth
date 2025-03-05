using System;
using UnityEngine;

public class LaserBlinking : MonoBehaviour
{
    public float onTime;
    public float offTime;
    float timer;

    Renderer laserRenderer;
    Light laserLight;

    void Awake()
    {
        laserRenderer = GetComponent<Renderer>();
        laserLight = GetComponent<Light>();
        timer = 0f;
    }

    void Update()
    {
        float dt = Time.deltaTime;
        timer += dt;

        if (laserRenderer.enabled && timer >= onTime)
        {
            SwitchBeam();
        }
        if (!laserRenderer.enabled && timer >= offTime)
        {
            SwitchBeam();
        }
    }

    void SwitchBeam()
    {
        timer = 0f;
        laserRenderer.enabled = !laserRenderer.enabled;
        laserLight.enabled = !laserLight.enabled;
    }
}
