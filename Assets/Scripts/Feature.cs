using System.Collections.Generic;
using UnityEngine;

public class Feature : MonoBehaviour {

    public int currentValue;
    public int currentHue;
    public List<Sprite> mAvailableParts;
    public List<Sprite> fAvailableParts;
    public List<Color> availableHues = new List<Color>() {Color.white};
    public SpriteRenderer sRenderer;

    public bool sex;

    public void RandomisePart() {
        sRenderer = GetComponent<SpriteRenderer>();
        currentValue = Random.Range(0, (sex)? fAvailableParts.Count : mAvailableParts.Count);
        currentHue = Random.Range(0, availableHues.Count);

        SetDisplay();
    }

    public void SetDisplay() {
        sRenderer.sprite = (sex)? fAvailableParts[currentValue] : mAvailableParts[currentValue];
        sRenderer.color = availableHues[currentHue];
    }

    public void Flip() {
        sRenderer.flipX = !sRenderer.flipX;
        transform.localPosition = new Vector3(-transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
    }

    public void SetSex(bool s) {
        sex = s;
    }

    public void Copy(Feature f) {
        availableHues = f.availableHues;
        fAvailableParts = f.fAvailableParts;
        mAvailableParts = f.mAvailableParts;
        currentHue = f.currentHue;
        currentValue = f.currentValue;
        sex = f.sex;
        SetDisplay();
    }

    void Start() {
        for (int i = 0; i < availableHues.Count; ++i) {
            var h = availableHues[i];
            availableHues[i] = new Color(h.r, h.g, h.b, 1);
        }
    }

    void Update() {

    }

}