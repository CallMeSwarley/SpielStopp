using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    public float geld;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        geld = 3000f;
    }

    // Update is called once per frame

    public void sellGame(float price=60f, int gameID = 0)
    {
        //todo remove game from database
        geld += price;
        leaveRegister();
    }
    public void buyGame(float price = 60f, int gameID = 0)
    {
        //todo add game from database
        geld -= price;
        leaveRegister();
    }
    public void leaveRegister()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
    public void enterRegister()
    {
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0.00001f;
    }
}
