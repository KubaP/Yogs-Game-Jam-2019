using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public struct Date {
    public DateTime time;

    public static string MonthToString(int m) {
        switch (m) {
            case 1: return "JAN";
            case 2: return "FEB";
            case 3: return "MAR";
            case 4: return "APR";
            case 5: return "MAY";
            case 6: return "JUN";
            case 7: return "JUL";
            case 8: return "AUG";
            case 9: return "SEP";
            case 10: return "OCT";
            case 11: return "NOV";
            case 12: return "DEC";
            default: return "NUL";
        }
    }

    public void Randomise(int low, int high) {
        time = time.AddYears(UnityEngine.Random.Range(low, high));
        time = time.AddMonths(UnityEngine.Random.Range(1, 13));
        time = time.AddDays(UnityEngine.Random.Range(1, 32));
    }

    public void Greater(Date date, int min, int max) {
        int m = time.Month;
        int d = time.Day;
        time = time.AddYears(UnityEngine.Random.Range(min - time.Year, max - time.Year));
        time = time.AddMonths(UnityEngine.Random.Range(0,12));
        time = time.AddDays(UnityEngine.Random.Range(0,31));
        if (time.Year > max) {
            time = time.AddYears(-2);
        }
    }

    public string LongDate() { 
        return time.Day.ToString().PadLeft(2, '0') + "/" + MonthToString(time.Month) + "/" + time.Year.ToString().PadLeft(4, '0').Substring(2,2);
    }

    public string ShortDate() { 
        return  time.Month.ToString().PadLeft(2, '0') + "/" + time.Year.ToString().Substring(2,2);
    }

    public Date(Date t) {
        time = t.time;
    }

    public Date(int y, int m, int d) {
        time = new DateTime(y, m, d);
    }

    public Date(Date start, int y, int m, int d) {
        time = new DateTime(start.time.Year, start.time.Month, start.time.Day);
        time = time.AddYears(y);
        time = time.AddMonths(m);
        time = time.AddDays(d);
    }
}

[System.Serializable]
public struct PersonData {
    public int nationality;
    public string fName;
    public string sName;
    public bool sex;
    public Date dob;
    public Date exp;
    public Date iss;
    public int ID;

    public PersonData(int pID) {
        nationality = 0;
        fName = "";
        sName = "";
        sex = false;
        dob = new Date();
        exp = new Date();
        iss = new Date();
        ID = pID;
    }

    public void GenerateData(int pID, Date today) {
        ID = pID;
        nationality = UnityEngine.Random.Range(0,5);
        sex = (UnityEngine.Random.Range(0,2) == 1);
        fName = Names.RandomFName(nationality, sex);
        sName = Names.RandomSName(nationality, sex);
        dob.Randomise(today.time.Year - 18 - 40, today.time.Year - 18);
        exp.Greater(today, today.time.Year, today.time.Year+9);
        iss = new Date(exp, -10, 0, 0);       
    } 
}

public class Person : MonoBehaviour
{
    public Feature head;
    public Feature hair;
    public Feature faceAccessory;
    public Feature torso;

    public PersonData personData;

    List<Feature> features;

    // Start is called before the first frame update
    void Start()
    {
        features = new List<Feature>() {head, hair, faceAccessory, torso};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewPerson() {
        foreach (Feature f in features) {
            f.SetSex(personData.sex);
            f.RandomisePart();
        }
    }

    public void SetData(PersonData data) {
        personData = data;
    }

    public void SetParts(Person p) {
        head.Copy(p.head);
        torso.Copy(p.torso);
        faceAccessory.Copy(p.faceAccessory);
        hair.Copy(p.hair);
    }

    public void Flip() {
        foreach (Feature f in features) {
            f.Flip();
        }
    }


}
