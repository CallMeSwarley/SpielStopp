using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Genre
{
    //If you add a new Genre dont forget to update the to String Method in the ProductDataManager
    None, //Default Case
    Action,
    Adventure,
    Arcade,
    Fighting,
    FPS,
    Koop,
    MOBA,
    Puzzle,
    Racing,
    RTS,
    RPG,
    Sandbox,
    Sports, 


}

class GenreGiver{
    public static Genre giveGenre()
    {
        int select = Random.Range(0, 13);
        return (Genre)select;
    }
}


    
    
