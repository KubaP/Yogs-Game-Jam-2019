using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct VisaData {
    public int id;
    public string fName;
    public string sName;
    public int nat;
    public bool sex;
    public Date travelDate;

    public string visaText;


    public void GenerateData(PersonData p, Date today) {
        id = p.ID;
        fName = p.fName;
        sName = p.sName;
        travelDate = today;
        nat = p.nationality;
        sex = p.sex;
        visaText = string.Format(
            "Dear Sir or Madam,\nI am writing to support the visa\napplication of {0} {1}.\n\nPlease let them enter your country.\nYours faithfully,\nThe {2} embassy",
            fName,
            sName,
            Visa.NationalityToString(nat)
        );
    }
}

public class Visa : Moveable
{

    public BoxCollider2D _collider;
    public GameObject largeVisa;
    public TextMesh visaText;
    public TextMesh dateText;

    public VisaData data;


    public Person picturePerson;

    public Stamp stamp;

    public AudioSource open;
    public AudioSource close;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        _collider = GetComponent<BoxCollider2D>();
        _collider.size = new Vector2(0.54f, 0.26f);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    new void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            largeVisa.SetActive(!largeVisa.activeInHierarchy); 
            if (largeVisa.activeInHierarchy) {
                open.Play();
                _collider.size = new Vector2(2.32f, 1.13f);
            }
            else {
                close.Play();
                _collider.size = new Vector2(0.54f, 0.26f);
            }
        }
        base.OnMouseOver();
    }

    public static string NationalityToString(int n) {
        switch(n) {
            case 0: return "Swiss";
            case 1: return "German";
            case 2: return "French";
            case 3: return "British";
            case 4: return "Polish";
            default: return "Nullish";
        }
    }

    public void AddTypos() {
        bool inserted = false;
        while (!inserted) {
            if (Random.Range(0,32) == 0) { data.visaText = data.visaText.Replace("writing", "writign"); inserted = true; }
            if (Random.Range(0,32) == 0) { data.visaText = data.visaText.Replace("enter", "entre"); inserted = true; }
            if (Random.Range(0,32) == 0) { data.visaText = data.visaText.Replace("embassy", "embasy"); inserted = true; }
            if (Random.Range(0,32) == 0) { data.visaText = data.visaText.Replace("support", "suppport"); inserted = true; }
            if (Random.Range(0,32) == 0) { data.visaText = data.visaText.Replace("Please", "please"); inserted = true; }
            if (Random.Range(0,32) == 0) { data.visaText = data.visaText.Replace("the visa", "the the visa"); inserted = true; }
            if (Random.Range(0,32) == 0) { data.visaText = data.visaText.Replace("faithfully", "faithfuly"); inserted = true; }
            if (Random.Range(0,32) == 0) { data.visaText = data.visaText.Replace("Dear", "dear"); inserted = true; }
        }
    }

    void ChangeName() {
        data.visaText = data.visaText.Replace(data.sName, Names.RandomSName(data.nat, data.sex));
        data.visaText = data.visaText.Replace(data.fName, Names.RandomFName(data.nat, data.sex));
    }

    public void SetData(PersonData p, Date today) {
        data.GenerateData(p, today);
    }

    public void Falsify() {
        switch(Random.Range(0,7)) {
            case 0:
                print ("V: Falsified Stamp");
                stamp.SetDisplay(false);
                break;
            case 1:
                print ("V: Falsified Name");
                ChangeName();
                break;
            case 2:
                print ("V: Added Typos");
                AddTypos();
                break;
            case 3:
                print ("V: Name & Stamp");
                ChangeName();
                stamp.SetDisplay(false);
                break;
            case 4:
                print ("V: Name, Typos, Stamp");
                AddTypos();
                ChangeName();
                stamp.SetDisplay(false);
                break;
            case 5:
                print ("V: Typos & Stamp");
                AddTypos();
                stamp.SetDisplay(false);
                break;
            case 6:
                print ("V: Name & Typos");
                ChangeName();
                AddTypos();
                break;
        }       
        stamp.RandomRotation();
    }

    public void SetDisplay(Person p) {
        visaText.text = data.visaText;
        dateText.text = data.travelDate.ShortDate();

        picturePerson.SetParts(p);
        picturePerson.torso.RandomisePart();
        picturePerson.torso.SetDisplay();
    }


    public void CloseVisa() {
        largeVisa.SetActive(false);
    }

}
