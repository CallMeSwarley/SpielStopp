using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProductDataManager : MonoBehaviour
{
    public TextAsset jsonFile;

    public List<string> TitleData = new List<string>();
    public List<Genre> Genre1Data = new List<Genre>();
    public List<Genre> Genre2Data = new List<Genre>();
    public List<int> RatingData = new List<int>();
    public List<int> TrendData = new List<int>();

    int maxIndex = -1;

    void Start()
    {
        Products JsonData = JsonUtility.FromJson<Products>(jsonFile.text);

        foreach (Product product in JsonData.products)
        {

            TitleData.Add(product.title);
            Genre1Data.Add(getGenre(product.genre1));
            Genre2Data.Add(getGenre(product.genre2));
            RatingData.Add(product.rating);
            TrendData.Add(product.rating);
            maxIndex++;
        }
    }

    public string getTitle(int index){
        if (index > maxIndex) {
            Debug.Log("Size Overflow Error");
            return null;
        }
        return TitleData[index];
    }

    public Genre getGenre1(int index)
    {
        if (index > maxIndex)
        {
            Debug.Log("Size Overflow Error");
            return Genre.None;
        }
        return Genre1Data[index];
    }

    public Genre getGenre2(int index)
    {
        if (index > maxIndex)
        {
            Debug.Log("Size Overflow Error");
            return Genre.None;
        }
        return Genre2Data[index];
    }

    public int getRating(int index)
    {
        if (index > maxIndex)
        {
            Debug.Log("Size Overflow Error");
            return 0;
        }
        return RatingData[index];
    }

    public int getTrend(int index)
    {
        if (index > maxIndex)
        {
            Debug.Log("Size Overflow Error");
            return 0;
        }
        return TrendData[index];
    }

    //This is a mess but if somebody has a better idea please share it
    public Genre getGenre(string value) {

        if (string.Equals(value, "Action"))
        {
            return Genre.Action;
        }
        else if (string.Equals(value, "Adventure"))
        {
            return Genre.Adventure;
        }
        else if (string.Equals(value, "Arcade"))
        {
            return Genre.Arcade;
        }
        else if (string.Equals(value, "Fighting"))
        {
            return Genre.Fighting;
        }
        else if (string.Equals(value, "FPS"))
        {
            return Genre.FPS;
        }
        else if (string.Equals(value, "Puzzle"))
        {
            return Genre.Puzzle;
        }
        else if (string.Equals(value, "Racing"))
        {
            return Genre.Racing;
        }
        else if (string.Equals(value, "RTS"))
        {
            return Genre.RTS;
        }
        else if (string.Equals(value, "RPG"))
        {
            return Genre.RPG;
        }
        else if (string.Equals(value, "Sandbox"))
        {
            return Genre.Sandbox;
        }
        else if (string.Equals(value, "Sports"))
        {
            return Genre.Sports;
        }else
        {
            return Genre.None;
        }    
    }
}
   