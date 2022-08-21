using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KundenSpawner : MonoBehaviour
{
    int spawntimer;
    float spawnCoolDown;
    [SerializeField]
    GameObject Customer;
    Display_UI UI;

    private void Start()
    {
        spawnCoolDown = 750;
        spawntimer = 0;
    }
    
    void Update()
    {
        /*spawntimer++;
            if (spawntimer >= spawnCoolDown)
            {
            spawnCustomer();
         }
        */
        if (Input.GetKeyDown("space")){
            spawnCustomer();
        }
    }

    void spawnCustomer()
    {
        Vector3 spawnLocation = new Vector3(-13, 2, -60);
        Instantiate(Customer, spawnLocation, Quaternion.Euler(0f, 0f, 0f));
        spawntimer = 0;
        nextSpawnIn();
    }

    void nextSpawnIn()
    {
        float i = UI.zufriedenheitsWert;
        spawnCoolDown = 500 + (i * 5);   
    }
}
