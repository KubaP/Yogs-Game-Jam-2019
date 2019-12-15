using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{

    public AudioClip musicQuiet;
    public AudioClip musicNormal;
    public AudioClip musicBusy;
    public AudioClip musicIntense;
    public AudioClip musicDeath;
    public AudioClip musicMarch;


    public AudioSource audioSourceA;
    public AnimationCurve fade;
    public AudioSource audioSourceB;
    public float intensity;
    bool playing;

    AudioClip nextClip;
    AudioClip currentClip;
    AudioSource currentSource;
    AudioSource otherSource;

    public SpriteRenderer _button;


    float fadeDuration;
    float endDuration;


    // Start is called before the first frame update
    void Start()
    {
        currentClip = musicQuiet;
        currentSource = audioSourceA;
        otherSource = audioSourceB;
        audioSourceA.clip = currentClip;
        audioSourceA.Play();
        fadeDuration = 0.0f;
        endDuration = 0.0f;
    }

    void Crossfade() {
        fadeDuration -= Time.deltaTime;
        currentSource.volume = 0.4f * ((fade.Evaluate(fadeDuration * 0.33f)));
        otherSource.volume = 0.4f * (1.0f - (fade.Evaluate(fadeDuration * 0.33f)));
        if (fadeDuration < 0.0f) {
            if (currentSource == audioSourceA) {
                currentSource = audioSourceB;
                otherSource = audioSourceA;
                currentClip = audioSourceB.clip;
            }
            else {
                currentSource = audioSourceA;
                otherSource = audioSourceB;
                currentClip = audioSourceA.clip;
            }
            
        }
    }

    void CrossfadeEnd() {
        endDuration -= Time.deltaTime;
        currentSource.volume = 0.4f * ((fade.Evaluate(endDuration)));
        otherSource.volume = 0.4f * (1.0f - (fade.Evaluate(endDuration)));
        if (fadeDuration < 0.0f) {
            if (currentSource == audioSourceA) {
                currentSource = audioSourceB;
                otherSource = audioSourceA;
                currentClip = audioSourceB.clip;
            }
            else {
                currentSource = audioSourceA;
                otherSource = audioSourceB;
                currentClip = audioSourceA.clip;
            }
            
        }
    }

    void CalculateNextClip()
    {
        if (intensity > 7.0f) {
            nextClip = musicMarch;
        }
        else if (intensity > 4.2f) {
            nextClip = musicDeath;
        }
        else if (intensity > 3.6f) {
            nextClip = musicIntense;
        }
        else if (intensity > 2.5f) {
            nextClip = musicBusy;
        }
        else if (intensity > 1.2f) {
            nextClip = musicNormal;
        }
        else {
            nextClip = musicQuiet;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateNextClip();
        if (fadeDuration > 0.0f) {
            Crossfade();
        }
        else if (endDuration > 0.0f) {
            CrossfadeEnd();
        }
        else if (nextClip != currentClip) {
            fadeDuration = 3.0f;
            otherSource.clip = nextClip;
            otherSource.Play();
        }
        else if (currentClip.length - currentSource.time < 3.0f) {
            endDuration = 1.0f;
            otherSource.clip = currentClip;
            otherSource.Play();
        }
    }

    public void SetMusic(int level) {
        switch(level) {
            case 0:
                nextClip = musicQuiet;
                break;
            case 1:
                nextClip = musicNormal;
                break;
            case 2:
                nextClip = musicBusy;
                break;
            case 3:
                nextClip = musicIntense;
                break;
            case 4:
                nextClip = musicDeath;
                break;
        }
    }

    public void ToggleRadio() {
        if (currentSource.volume == 0.4f) {
            currentSource.volume = 0.0f;
            otherSource.volume = 0.0f;
            _button.color = Color.red;
        }
        else {
            currentSource.volume = 0.4f;
            otherSource.volume = 0.0f;
            _button.color = Color.green;
        }
    }
}

