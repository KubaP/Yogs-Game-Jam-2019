using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weirdos : MonoBehaviour
{

    public AudioClip[] clips;
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying) {
            source.clip = clips[Random.Range(0, clips.Length)];
            source.Play();
        }
    }
}
