using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NameList {
    public string[] mNames = {};
    public string[] fNames = {};
    public string[] sNames = {};

    public NameList(string[] m, string[] f, string[] s) {
        mNames = m;
        fNames = f;
        sNames = s;
    }
}

public class Names {

    public static NameList[] nameLists =
    {
        // Swiss
        new NameList(
            new string[] {"Luca", "Julien", "Romain", "Jonathan", "Jonas", "Simon", "Joel", "Antonio", "Frédéric", "Samuel"},
            new string[] {"Sarah", "Léa", "Nina", "Sara", "Julie", "Anna", "Gaelle", "Marie", "Céline", "Michelle"},
            new string[] {"Bianchi", "Bernasconi", "Fontana", "Crivelli", "Galli", "Müller", "Meier", "Schmid", "Keller", "Weber"}
        ),

        // German
        new NameList(
            new string[] {"Alex", "Lukas", "Michael", "Daniel", "Philipp", "Jonas", "Fabian", "Marcel", "Tim", "Kevin"},
            new string[] {"Lea", "Julia", "Laura", "Anna", "Lisa", "Lena", "Sarah", "Katharina", "Johanna", "Sophie"},
            new string[] {"Müller", "Schmidt", "Schneider", "Fischer", "Meyer", "Weber", "Schulz", "Wagner", "Becker", "Hoffman"}
        ),

        // French
        new NameList(
            new string[] {"Thomas", "Nicolas", "Julien", "Quentin", "Maxime", "Alexandre", "Antoine", "Kevin", "Clement", "Romain"},
            new string[] {"Marie", "Camille", "Léa", "Manon", "Chloé", "Laura", "Julie", "Sarah", "Pauline", "Mathilde"},
            new string[] {"Martin", "Bernard", "Dubois", "Thomas", "Robert", "Richard", "Petit", "Durand", "Leroy", "Moreau"}
        ),

        // British
        new NameList(
            new string[] {"James", "Jack", "Alex", "Ben", "Daniel", "Tom", "Adam", "Ryan", "Sam", "Matthew"},
            new string[] {"Emily", "Chloe", "Megan", "Charlotte", "Emma", "Lauren", "Hannah", "Ellie", "Sophie", "Katie"},
            new string[] {"Smith", "Jones", "Williams", "Taylor", "Davies", "Evans", "Thomas", "Johnson"}
        ),

        // Polish
        new NameList(
            new string[] {"Kacper", "Adam", "Mateusz", "Bartek", "Szymon", "Kamil", "Przemek", "Dawid", "Tomek", "Kuba"},
            new string[] {"Julia", "Natalia", "Wiktoria", "Dominika", "Karolina", "Aleksandra", "Kasia", "Paulina", "Weronika", "Ola"},
            new string[] {"Nowak", "Kowalski", "Wisniewski", "Wojcik", "Kowalczyk", "Kaminski", "Lewandowski", "Zielinski", "Szymanski", "Wozniak"}
        ),
    };

    public static string RandomFName(int nationality, bool sex) {
        NameList n = nameLists[nationality];
        if (sex) {
            return n.fNames[Random.Range(0, n.fNames.Length)];
        }
        return n.mNames[Random.Range(0, n.mNames.Length)];
    }

    public static string RandomSName(int nationality, bool sex) {
        NameList n = nameLists[nationality];
        return n.sNames[Random.Range(0, n.sNames.Length)];
    }
}