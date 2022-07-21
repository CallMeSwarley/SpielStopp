using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    string Name;
    Genre genre1;
    Genre genre2;
    int id;
    int rating;
    int trend;

    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void setValues(string nName, Genre nGenre1, Genre nGenre2, int nid, int nrating, int ntrend) {
        Name = nName;
        genre1 = nGenre1;
        genre2 = nGenre2;
        id = nid;
        rating = nrating;
        trend = ntrend;
    }
}

