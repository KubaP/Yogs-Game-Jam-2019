using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerFlicker : MonoBehaviour
{

    public SpriteRenderer _renderer;
    [Range(0.0f, 1.0f)]
    public float max = 1.0f;
    [Range(0.0f, 1.0f)]
    public float min = 0.8f;
    [Range(1, 60)]
    public int flickerInterval = 10;
    int flickerFrames = 0;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flickerFrames % flickerInterval == 0) {
            float h, s, v;
            Color.RGBToHSV(_renderer.color, out h, out s, out v);
            _renderer.color = Color.HSVToRGB(h, Random.Range(min, max), v);
        }
        ++flickerFrames;
    }
}
