using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{

    public Camera _camera;
    Vector3 n;
    Vector3 a;

    public bool mouseOn = false;

    // Start is called before the first frame update
    protected void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (mouseOn) {
            if (Input.GetMouseButtonUp(0)) {
                mouseOn = false;
            }
            n = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
            n.x = Mathf.Clamp(n.x, -2.0f, 2.0f);
            n.y = Mathf.Clamp(n.y, -1.5f, 1.5f);
            n.x += a.x;
            n.y += a.y;
            this.transform.position = new Vector3(n.x, n.y, this.transform.position.z);
        }
    }

    protected void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            mouseOn = true;
            a = transform.position - _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
        }
    }

}
