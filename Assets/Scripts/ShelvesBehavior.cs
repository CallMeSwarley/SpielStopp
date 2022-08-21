using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelvesBehavior : MonoBehaviour
{
    public static List<int> Inventory = new List<int>();


    void stockGame(int ID) {
        Inventory.Add(ID);
    }

    //Returns true if a game was succesfully taken and false if nothing could be removed
    bool takeGame(int ID) {
        if (Inventory.Contains(ID)) {
            Inventory.Remove(ID);
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
