using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Letterbox : MonoBehaviour
{

    [SerializeField]
    Vector2Int targetAspectRatio = new Vector2Int(4,3);

    Resolution currentResolution;

    Camera m_camera;

    // Start is called before the first frame update
    void Start()
    {
        currentResolution = Screen.currentResolution;
        m_camera = GetComponent<Camera>();
        SetLetterbox();
    }

    void Update() {
        if (currentResolution.height != Screen.height) {
            if (currentResolution.width != Screen.width) {
                SetLetterbox();
                currentResolution = Screen.currentResolution;
            }
        }
    }

    void SetLetterbox() {
        float currentRatio = Screen.width / (float) Screen.height;
        float targetRation = targetAspectRatio.x / (float) targetAspectRatio.y;

        if (Mathf.Approximately(currentRatio, targetRation)) {
            m_camera.rect = new Rect(0,0,1,1);
        }
        else if(currentRatio > targetRation) {
            float normalizedWidth = targetRation / currentRatio;
            float barThickness = (1f - normalizedWidth)/2f;
            m_camera.rect = new Rect(barThickness, 0, normalizedWidth, 1);
        }
        else {
            float normalizedHeight = currentRatio / targetRation;
            float barThickness = (1f - normalizedHeight) / 2f;
            m_camera.rect = new Rect(0, barThickness, 1, normalizedHeight);
        }
    }
}
