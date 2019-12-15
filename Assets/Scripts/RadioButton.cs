using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioButton : MonoBehaviour
{

    public Radio radio;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void OnMouseOver() {
        if(Input.GetMouseButtonDown(0)) {
            radio.ToggleRadio();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
