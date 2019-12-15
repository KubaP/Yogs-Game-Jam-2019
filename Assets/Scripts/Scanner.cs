using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{

    enum State {Up, Down, MovingUp, MovingDown};
    State state = State.Down;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            switch (state) {
                case State.Down:
                    state = State.MovingUp;
                    break;
                case State.Up:
                    state = State.MovingDown;
                    break;
                case State.MovingDown:
                    state = State.MovingUp;
                    break;
                case State.MovingUp:
                    state = State.MovingDown;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case State.MovingDown:
                if (transform.position.y < -2.27f) {
                    state = State.Down;
                }
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(-1.24f, -2.28f, 0.0f), 6.0f * Time.deltaTime);
                break;
            case State.MovingUp:
                if (transform.position.y > -0.72f) {
                    state = State.Up;
                }
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(-1.24f, 0.72f, 0.0f), 6.0f * Time.deltaTime);
                break;
        }
    }
}
