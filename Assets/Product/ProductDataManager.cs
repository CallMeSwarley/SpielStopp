using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ProductDataManager : MonoBehaviour
{
    public TextAsset jsonFile;

    public static List<string> TitleData = new List<string>();
    public static List<Genre> Genre1Data = new List<Genre>();
    public static List<Genre> Genre2Data = new List<Genre>();
    public static List<int> RatingData = new List<int>();
    public static List<int> TrendData = new List<int>();

    int maxIndex = -1;

    void Start()
    {
        //Json Reader adapted from https://forum.unity.com/threads/how-to-read-json-file.401306/
        string path = Application.streamingAssetsPath + "/products.json";
        string data = File.ReadAllText(path);
        Products JsonData = JsonUtility.FromJson<Products>(data);

        foreach (Product product in JsonData.products)
        {
            TitleData.Add(product.title);
            Genre1Data.Add(getGenre(product.genre1));
            Genre2Data.Add(getGenre(product.genre2));
            RatingData.Add(product.rating);
            TrendData.Add(product.trend);
            maxIndex++;
        }
    }

    void OnApplicationQuit()
    {
        TitleData.Clear();
        Genre1Data.Clear();
        Genre2Data.Clear();
        RatingData.Clear();
        TrendData.Clear();
    }

    private void Update()
    {
        
        if (Input.GetKeyDown("u")) {
            TitleData.Clear();
            Genre1Data.Clear();
            Genre2Data.Clear();
            RatingData.Clear();
            TrendData.Clear();
            Start();
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
        else if (string.Equals(value, "Koop"))
        {
            return Genre.Koop;
        }
        else if (string.Equals(value, "MOBA"))
        {
            return Genre.MOBA;
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
   