using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KundenSpawner : MonoBehaviour
{
    int spawntimer;
    float spawnCoolDown;
    [SerializeField]
    GameObject Customer;

    private void Start()
    {

        spawnCustomer();

    }
    
    void Update()
    {
       spawntimer++;
            if (spawntimer >= spawnCoolDown)
            {
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
        float i = Display_UI.zufriedenheitsWert;
        spawnCoolDown = 350 + (i * 5);   
    }
}
