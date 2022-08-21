using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestockPC : MonoBehaviour, Interactable
{

    public GameObject BuyMenuUI;
    private bool inDialog = false;
    public void endInteract()
    {
        
        
    }

    public void Interact()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BuyGames();
        }
    }

    public void BuyGames()
    {
        
        BuyMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        inDialog = Time.timeScale == 0.00001f;
        Time.timeScale = 0.00001f;//freezes time in game       
    }


    public bool IsCurrentlyInteractable()
    {
        return true;
    }

}
