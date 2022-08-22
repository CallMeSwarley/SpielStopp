using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    public NPC_master npc_master;
    public InteractionManagerNPC interactionManagerNPC;
    public static float geld;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        geld = 3000f;
    }

    // Update is called once per frame

    public void sellGame(float price=60.0f, int gameID = 0)
    {
        //todo remove game from database
        if (npc_master != null)
        {
            npc_master.currentState = NPC_master.state.satisfied;
        }
        Debug.Log(price);
        geld += price;
        leaveRegister();
    }
    public void buyGame(float price = 60.0f, int gameID = 0)
    {
        //todo add game from database
        if (npc_master != null)
        {
            npc_master.currentState = NPC_master.state.satisfied;
        }
        Debug.Log(price);
        geld -= price;
        leaveRegister();
    }
    public void leaveRegister()
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        interactionManagerNPC.interactable = true;
    }
    public void enterRegister()
    {
        if (!gameObject.active)
        {
            Debug.Log("activate register");
            gameObject.SetActive(true);
            interactionManagerNPC.interactable = false;
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0.00001f;
    }
}
