using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{

    string title;
    Genre genre1;
    Genre genre2;
    int rating;
    int trend;
    public GameInstance(string newTitle, Genre genre1New, Genre genre2New, int ratingNew, int trendNew) {
        title = newTitle;
        genre1 = genre1New;
        genre2 = genre2New;
        rating = ratingNew;
        trend = trendNew;
    }
}
