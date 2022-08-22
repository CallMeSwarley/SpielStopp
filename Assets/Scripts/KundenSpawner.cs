using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KundenSpawner : MonoBehaviour
{
    [SerializeField]
    float spawntimer;
    float spawnCoolDown;
    [SerializeField]
    GameObject Customer;

    private void Start()
    {
        spawnCustomer();
    }

    void Update()
    {

        spawntimer = (spawntimer) + (1 * Time.deltaTime);
        if (spawntimer >= spawnCoolDown)
        {
            spawnCustomer();
        }

        if (Input.GetKeyDown("space"))
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
        spawnCoolDown = 5 + i;
    }
}
