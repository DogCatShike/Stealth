using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;
    bool sceneStarting = true;
    RawImage rawImage = null;

    void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    void Update()
    {
        float dt = Time.deltaTime;

        if (sceneStarting)
        {
            StartScene(dt);
        }
    }

    void FadeToClear(float dt)
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, fadeSpeed * dt);
    }

    void FadeToBlack(float dt)
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.black, fadeSpeed * dt);
    }

    void StartScene(float dt)
    {
        FadeToClear(dt);
        if (rawImage.color.a <= 0.05f)
        {
            rawImage.color = Color.clear;
            rawImage.enabled = false;
            sceneStarting = false;
        }
    }

    public void EndScene(float dt)
    {
        rawImage.enabled = true;
        FadeToBlack(dt);
        if (rawImage.color.a >= 0.95f)
        {
            SceneManager.LoadScene(0);
        }
    }
}
