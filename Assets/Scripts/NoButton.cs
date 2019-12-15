using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoButton : MonoBehaviour
{

    public Sprite down;
    public Sprite up;
    public Controller controller;
    public SpriteRenderer _renderer;
    public AudioSource noise;

    float upTime;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (upTime > 0.0) {
            upTime -= Time.deltaTime;
            if (upTime < 0.0) {
                _renderer.sprite = up;
            }
        }
    }

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            controller.ProcessPerson(false);
            upTime = 0.2f;
            _renderer.sprite = down;
            noise.Play();
        }
    }

}
