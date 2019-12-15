using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

    public Vector3 a;
    public Vector3 b;

    bool dir = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dir) {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, a, Time.deltaTime * 0.1f);
        }
        else {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, b, Time.deltaTime * 0.1f);
        }
        if (this.transform.localPosition == a) {
            dir = false;
        }
        if (this.transform.localPosition == b) {
            dir = true;
        }
    }
}
