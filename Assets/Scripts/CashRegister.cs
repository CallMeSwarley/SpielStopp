using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    public NPC_master npc_master;
    public InteractionManagerNPC interactionManagerNPC;
    public static float geld;
    public float price;
    RestockPC buyer;
    int buyID;
    // Start is called before the first frame update
    void Start()
    {
        buyer = GetComponentInParent<RestockPC>();
        gameObject.SetActive(false);
        geld = 500f;
    }

    // Update is called once per frame

    public void sellGame()
    {
        float price = 60f;
        //todo remove game from database
        if (npc_master != null)
        {
            npc_master.currentState = NPC_master.state.satisfied;
        }
        Debug.Log(price);
        geld += price;
    }
    public void buyGame()
    {
        //todo add game from database
        if (npc_master != null)
        {
            npc_master.currentState = NPC_master.state.satisfied;
        }
        Debug.Log(price);
        geld -= price;
        buyer.BuyGame(buyID);
        

    }
    
}
