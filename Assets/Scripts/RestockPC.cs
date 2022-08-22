using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RestockPC : MonoBehaviour, Interactable
{

    public GameObject BuyMenuUI;
    public TMP_Text Titel1;
    public TMP_Text Titel2;
    public TMP_Text Titel3;
    public GameObject ShelfMenuUI;
    //public GameObject ShelfGroupEG1, ShelfGroupEG2, ShelfGroupEG3, ShelfGroupEG4, ShelfGroupEG5, ShelfGroupEG6, ShelfGroupOG1, ShelfGroupOG2;
    int GameOneID;
    int GameTwoID;
    int GameThreeID;
    int ToStock;
    bool active = true;
    
    public void endInteract()
    {
        
        
    }

    public void Interact()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameOneID = UnityEngine.Random.Range(0, ProductDataManager.TitleData.Count);
            GameTwoID = UnityEngine.Random.Range(0, ProductDataManager.TitleData.Count);
            GameThreeID = UnityEngine.Random.Range(0, ProductDataManager.TitleData.Count);
            active = false;
            OpenGamesUI();
        }
    }

    public void OpenGamesUI()
    {
        var stringOne = new System.Text.StringBuilder();
        stringOne.Append(ProductDataManager.TitleData[GameOneID] + "\n");
        stringOne.Append(ProductDataManager.Genre1Data[GameOneID] + ", ");
        stringOne.Append(ProductDataManager.Genre1Data[GameOneID] + ", ");
        stringOne.Append("Rating:" + ProductDataManager.RatingData[GameOneID] + " ");
        stringOne.Append("Trend:" + ProductDataManager.TrendData[GameOneID]);

        var stringTwo = new System.Text.StringBuilder();
        stringTwo.Append(ProductDataManager.TitleData[GameTwoID] + "\n");
        stringTwo.Append(ProductDataManager.Genre1Data[GameTwoID] + ", ");
        stringTwo.Append(ProductDataManager.Genre1Data[GameTwoID] + ", ");
        stringTwo.Append("Rating:" + ProductDataManager.RatingData[GameTwoID] + " ");
        stringTwo.Append("Trend:" + ProductDataManager.TrendData[GameTwoID]);

        var stringThree = new System.Text.StringBuilder();
        stringThree.Append(ProductDataManager.TitleData[GameThreeID] + "\n");
        stringThree.Append(ProductDataManager.Genre1Data[GameThreeID] + ", ");
        stringThree.Append(ProductDataManager.Genre1Data[GameThreeID] + ", ");
        stringThree.Append("Rating:" + ProductDataManager.RatingData[GameThreeID] + " ");
        stringThree.Append("Trend:" + ProductDataManager.TrendData[GameThreeID]);

        Titel1.text = stringOne.ToString(); ;
        Titel2.text = stringTwo.ToString();
        Titel3.text = stringThree.ToString();
        BuyMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0.00001f;//freezes time in game
        
    }

    public void BuyGame(int ID) {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0.00001f;//freezes time in game
        BuyMenuUI.SetActive(false);
        switch (ID) {
            case 1:
                ToStock = GameOneID;
                
                break;
            case 2:
                ToStock = GameTwoID;
                break;
            case 3:
                ToStock = GameThreeID;
                break;
        }
        ShelfMenuUI.SetActive(true);
    }

    public void PlaceInShelf(GameObject Shelf){
        Shelf.GetComponent<ShelvesBehavior>().stockGame(ToStock);
        resumeGame();
    }


    public void Cancel() {
        resumeGame();
    }

    public bool IsCurrentlyInteractable()
    {
        return active;
    }

    public void resumeGame() {
        BuyMenuUI.SetActive(false);
        ShelfMenuUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        active = true;
    }

}
