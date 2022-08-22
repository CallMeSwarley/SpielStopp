using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelvesBehavior : MonoBehaviour
{
    [SerializeField]
    public List<int> Inventory = new List<int>();
    [SerializeField]
    private GameObject littleOne;
    [SerializeField]
    private GameObject littleTwo;
    [SerializeField]
    bool ThreeFlag;
    [SerializeField]
    private GameObject littleThree;


    private void Start()
    {
        littleOne.SetActive(false);
        littleTwo.SetActive(false);
        if (ThreeFlag){
            littleThree.SetActive(false);
        }
    }

    public void stockGame(int ID) {
        Inventory.Add(ID);
        littleOne.SetActive(true);
        littleTwo.SetActive(true);
        if (ThreeFlag)
        {
            littleThree.SetActive(true);
        }

    }

    //Returns true if a game was succesfully taken and false if nothing could be removed
     public bool takeGame(int ID) {
        if (Inventory.Contains(ID)) {
            Inventory.Remove(ID);
            if (Inventory.Count == 0) {
                littleOne.SetActive(false);
                littleTwo.SetActive(false);
                if (ThreeFlag)
                {
                    littleThree.SetActive(false);
                }
            }
            return true;
        }
        return false;
    }

    public string ReturnInventory() {
        var BobTheStringBuilder = new System.Text.StringBuilder();
        foreach (int ID in Inventory) {
            BobTheStringBuilder.Append(ProductDataManager.TitleData[ID]);
            BobTheStringBuilder.Append("\n");
        }
        return BobTheStringBuilder.ToString();
    }

}
