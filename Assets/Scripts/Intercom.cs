using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intercom : MonoBehaviour
{

    public AudioClip[] intercoms;

    public AudioSource BingBong;
    public AudioSource IC;


    private float timeToNextIntercom = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToNextIntercom -= Time.deltaTime;
        if (timeToNextIntercom < 0) {
            timeToNextIntercom = Random.Range(30f, 120f);
            BingBong.Play();
            IC.clip = intercoms[Random.Range(0, intercoms.Length)];
            IC.PlayDelayed(1.0f);
        }
    }
}
