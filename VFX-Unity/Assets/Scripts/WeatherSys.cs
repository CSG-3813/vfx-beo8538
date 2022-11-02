/***
 * Author: Betzaida
 * Created: 11-2-2022
 * Modified:
 * Description: controls weather effects
 ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WeatherSys : MonoBehaviour
{
    public GameObject weatherGO;
    ParticleSystem rainPS; //rain particle effect

    public float rainTime = 10; //how long it rains

    public AudioMixerSnapshot raining;
    public AudioMixerSnapshot sunny;

    float timerTime; //timer to control the rain
    bool startTime; //starts the time for when it rains
    AudioSource audioSrc;

    bool isRaining;
    public bool IsRaining { get { return isRaining; } }

    public Volume rainProcess;

    float lerpValue;
    float lerpDuration = 10;
    float transitionTime;


    // Start is called before the first frame update
    void Start()
    {
        rainPS = weatherGO.GetComponent<ParticleSystem>();
        audioSrc = weatherGO.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (startTime)
        {
            if (timerTime > 0)
            {
                timerTime -= Time.deltaTime;
                tintSky();
            }
            else
            {
                EndRain();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter Rain");
        if(other.tag == "Player")
        {
            if (!startTime)
            {
                timerTime = rainTime;
                startTime = true;
                isRaining = true;
                rainPS.Play();
                audioSrc.Play();

                raining.TransitionTo(2.0f);

            }
        }
    }

    void EndRain()
    {
        startTime = false;
        isRaining = false;
        rainPS.Stop();
        audioSrc.Stop();
        sunny.TransitionTo(2.0f);
    }

    void tintSky()
    {
        Debug.Log("tint");
        if(transitionTime < lerpDuration)
        {
            lerpValue = Mathf.Lerp(0, 1, transitionTime / lerpDuration);
            transitionTime += Time.deltaTime;
            rainProcess.weight = lerpValue;

        }
    }

}
