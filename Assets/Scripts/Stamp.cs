using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour
{

    public Sprite[] real;
    public Sprite[] fake;

    public SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer> ();
    }

    public void SetDisplay(bool r) {
        if (r) {
            _renderer.sprite = real[Random.Range(0, real.Length)];
        }
        else {
            _renderer.sprite = fake[Random.Range(0, fake.Length)];
        }
    }

    public void RandomRotation() {
        this.transform.rotation = Quaternion.identity;
        transform.Rotate(0f, 0f, Random.Range(-5f, 5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
