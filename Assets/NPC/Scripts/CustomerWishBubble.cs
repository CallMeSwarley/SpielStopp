using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWishBubble : MonoBehaviour
{
    int wantedGameId; 
    bool wantedGameNotHere;
    
    void Awake() {
        wantedGameId = selectWishedGame();
       
    }
int selectWishedGame()
{

    //Generiert Wunschliste vom Kunden entweder basierend auf Rating, Trend oder Genre der Spiele. 
    int random = UnityEngine.Random.Range(0, 3);
    switch (random)
    {
        //Nachteil: Nimmt immer erstes spiel in der liste bei jeder der methoden. TODO: Ein spiel aus Pool mit richtigen Bewertungen aussuchen? 
        case 0:
            return selectFromRating();

        case 1:
            return selectFromTrend();
        case 2:
            return selectFromGenre();
    }
    return 0;
}

int selectFromRating()
{
        List<int> PotentialGames = new List<int>();
        //Kunde will nur spiele mit Rating 4-5
        for(int i=0; i <ProductDataManager.RatingData.Count;  i++) {
           Debug.Log(i);
           if (ProductDataManager.RatingData[i] >= 4) {
                PotentialGames.Add(i);
           }
        }
        if (PotentialGames.Count > 0){
            int GameId = UnityEngine.Random.Range(0, PotentialGames.Count);
            return GameId;
        }
        return 0;
}

int selectFromTrend()
{
        List<int> PotentialGames = new List<int>();
        //Kunde will nur spiele mit Trend 4-5
        for (int i = 0; i < ProductDataManager.TrendData.Count; i++)
        {
            Debug.Log(i);
            if (ProductDataManager.TrendData[i] >= 4)
            {
                PotentialGames.Add(i);
            }
        }
        if (PotentialGames.Count > 0)
        {
            int GameId = UnityEngine.Random.Range(0, PotentialGames.Count);
            return GameId;
        }
        return 0;
    }

    int selectFromGenre()
    {
        Genre wanted = GenreGiver.giveGenre();

        List<int> PotentialGames = new List<int>();
        //Kunde will nur spiele mit Trend 4-5
        for (int i = 0; i < ProductDataManager.Genre1Data.Count; i++)
        {
            Debug.Log(i);
            if (ProductDataManager.Genre1Data[i] == wanted || ProductDataManager.Genre2Data[i] == wanted)
            {
                PotentialGames.Add(i);
            }
        }
        if (PotentialGames.Count > 0)
        {
            int GameId = UnityEngine.Random.Range(0, PotentialGames.Count);
            return GameId;
        }
        return 0;
    }
    
}
