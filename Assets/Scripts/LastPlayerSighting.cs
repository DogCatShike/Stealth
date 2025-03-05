using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlayerSighting : MonoBehaviour
{
    // 位置
    public Vector3 position = new Vector3(1000, 1000, 1000);
    public Vector3 resetPosition = new Vector3(1000, 1000, 1000);

    // 灯光
    public float lightHighIntensity = 0.25f;
    public float lightLowIntensity = 0f;
    public float fadeSpeed = 7f;

    // 音乐
    public float musicFadeSpeed = 1;
    AlarmLight alarmScript; // 警报灯
    Light mainLight;
    AudioSource music;
    AudioSource panicAudio;
    AudioSource[] sirens; // 警报
    const float muteVolume = 0f;
    const float normalVolume = 0.8f;

    void Awake()
    {
        alarmScript = GameObject.FindWithTag(Tags.AlarmLight).GetComponent<AlarmLight>();
        mainLight = GameObject.FindWithTag(Tags.MainLight).GetComponent<Light>();
        music = GetComponent<AudioSource>();
        panicAudio = transform.Find("secondary_music").GetComponent<AudioSource>();

        GameObject[] sirenGameObjects = GameObject.FindGameObjectsWithTag(Tags.Siren);
        sirens = new AudioSource[sirenGameObjects.Length];
        for (int i = 0; i < sirens.Length; i++)
        {
            sirens[i] = sirenGameObjects[i].GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        float dt = Time.deltaTime;

        SwitchAlarms(dt);
        MusicFading(dt);
    }

    void SwitchAlarms(float dt)
    {
        alarmScript.alarmOn = position != resetPosition;

        float newIntensity;
        if (position != resetPosition)
        {
            newIntensity = lightLowIntensity;
        }
        else
        {
            newIntensity = lightHighIntensity;
        }
        mainLight.intensity = Mathf.Lerp(mainLight.intensity, newIntensity, fadeSpeed * dt);

        for (int i = 0; i < sirens.Length; ++i)
        {
            if (position != resetPosition && sirens[i].isPlaying)
            {
                sirens[i].Play();
            }
            else if (position == resetPosition)
            {
                sirens[i].Stop();
            }
        }
    }

    void MusicFading(float dt)
    {
        if (position != resetPosition)
        {
            music.volume = Mathf.Lerp(music.volume, muteVolume, musicFadeSpeed * dt);
            Debug.Log(music.volume);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, normalVolume, musicFadeSpeed * dt);
        }
        else
        {
            music.volume = Mathf.Lerp(music.volume, normalVolume, musicFadeSpeed * dt);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, muteVolume, musicFadeSpeed * dt);
        }
    }
}
