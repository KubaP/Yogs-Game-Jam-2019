using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handbook : Moveable
{

    BoxCollider2D _collider;
    public GameObject largeHandbook;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        _collider = GetComponent<BoxCollider2D>();
        _collider.size = new Vector2(0.44f, 0.61f);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    new void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            largeHandbook.SetActive(!largeHandbook.activeInHierarchy); 
            if (largeHandbook.activeInHierarchy) {
                _collider.size = new Vector2(2.3f, 2.23f);
            }
            else {
                _collider.size = new Vector2(0.44f, 0.61f);
            }
        }
        base.OnMouseOver();
    }
}
