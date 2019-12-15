using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passport : Moveable
{

    public GameObject largePassport;
    public BoxCollider2D _collider;
    public SpriteRenderer passport;
    public SpriteRenderer securityFeature;
    public Person picturePerson;

    public TextMesh sNameText;
    public TextMesh fNameText;

    public TextMesh dobText;
    public TextMesh issText;
    public TextMesh expText;
    public TextMesh sexText;
    public TextMesh codeText;

    
    public PersonData data;


    [System.Serializable]
    public struct PassportCollection {
        public Sprite normal;
        public Sprite torn;
        public Sprite fake;
    }

    [System.Serializable]
    public struct FeatureCollection {
        public Sprite normal;
        public Sprite fake1;
        public Sprite fake2;
    }

    public List<PassportCollection> passports;
    public List<FeatureCollection> features;
    

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        _collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    new void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            largePassport.SetActive(!largePassport.activeInHierarchy); 
            if (largePassport.activeInHierarchy) {
                _collider.size = new Vector2(1.2f, 1.5f);
            }
            else {
                _collider.size = new Vector2(0.2f, 0.25f);
            }
        }
        base.OnMouseOver();
    }

    static string NationalityToString(int n) {
        switch (n) {
            case 0: return "CHE";
            case 1: return "DEU";
            case 2: return "FRA";
            case 3: return "GBR";
            case 4: return "POL";
            default: return "NUL";
        }
    }

    public void Falsify() {
        int fakePasses = Random.Range(1, 3);
        int newNat = Random.Range(0, 5);
        while (newNat == data.nationality) {
            newNat = Random.Range(0, 5);
        }

        for (int i = 0; i < fakePasses; ++i) {
            int property = Random.Range(0, 7);
            switch(property) {
                case 0:
                    print ("PP: Falsified Appearance");
                    int nP = Random.Range(0,3);
                    if (nP == 0) {
                        passport.sprite = passports[data.nationality].torn;
                    }
                    else if (nP == 1) {
                        passport.sprite = passports[data.nationality].fake;
                    }
                    else {
                        passport.sprite = passports[newNat].normal;
                    }
                    break;
                case 1:
                    print ("PP: Falsified Nationality");
                    data.nationality = newNat;
                    break;
                case 2:
                    print ("PP: Falsified Expiry");
                    data.exp = new Date(data.iss, 0, 0, Random.Range(-1000, 1));
                    break;
                case 3:
                    print ("PP: Falsified Sex");
                    data.sex = !data.sex;
                    break;
                case 4:
                    print ("PP: Falsified Issue");
                    data.iss.Greater(new Date(2019, 1, 1), 1, 2025);
                    break;
                case 5:
                    print ("PP: Falsified Nationality");
                    data.nationality = newNat;
                    break;
                case 6:
                    print ("PP: Falsified Security");
                    int actual = data.nationality / 2;
                    int mP = Random.Range(0,3);
                    if (mP == 0) {
                        securityFeature.sprite = features[actual].fake1;
                    }
                    else if (mP == 1) {
                        securityFeature.sprite = features[actual].fake2;
                    }
                    else {
                        if (actual == 0) {
                            securityFeature.sprite = features[1].normal;
                        }
                        else {
                            securityFeature.sprite = features[0].normal;
                        }
                    }
                    break;
                case 7:
                    int piece = Random.Range(0, 4);
                    switch (piece) {
                        case 0:
                            picturePerson.head.RandomisePart();
                            picturePerson.head.SetDisplay();
                            break;
                        case 1:
                            picturePerson.hair.RandomisePart();
                            picturePerson.hair.SetDisplay();
                            break;
                        case 2:
                            picturePerson.faceAccessory.RandomisePart();
                            picturePerson.faceAccessory.SetDisplay();
                            break;
                    }
                    break;
            }
        }
    }

    public void SetData(PersonData d) {
        data.dob = d.dob;
        data.exp = d.exp;
        data.fName = d.fName;
        data.ID = d.ID;
        data.iss = d.iss;
        data.nationality = d.nationality;
        data.sex = d.sex;
        data.sName = d.sName;
    }

    public void SetSprites(Person p) {
        passport.sprite = passports[data.nationality].normal;
        securityFeature.sprite = features[data.nationality / 3].normal;
        picturePerson.SetParts(p);
        picturePerson.torso.RandomisePart();
        picturePerson.torso.SetDisplay();
    }

    public void SetDisplay(Person p) {
        sNameText.text = data.sName;
        fNameText.text = data.fName;
        dobText.text = data.dob.LongDate();
        sexText.text = (data.sex) ? "F" : "M";
        issText.text = data.iss.LongDate();
        expText.text = data.exp.LongDate();
        codeText.text = NationalityToString(data.nationality);

    }

    public void ClosePassport() {
        largePassport.SetActive(false);
    }
}
