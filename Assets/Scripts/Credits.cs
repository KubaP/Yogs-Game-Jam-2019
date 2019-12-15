using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{


    public Vector3 startPosition;
    public Vector3 endPosition;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0f, 30.0f * Time.deltaTime, 0.0f);
        if (transform.localPosition.y > endPosition.y) {
            transform.localPosition = startPosition;
        }
    }
}
