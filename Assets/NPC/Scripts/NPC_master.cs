using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_master : MonoBehaviour //for all the stats & bahaviour of the npc
{
    // Satisfaction
    public float satisfactionLvl;
    public float maxSatisfaction;
    [Range(0.1f, 1.0f)]
    public float desatisfactionSpeed = 1f;

    // wish
    public bool hasWish;//wunscherfüllung wir hier in jeweiligen methoden passieren
    public bool receivesHelp;
    [HideInInspector]
    public enum state //for switch stmts later
    {
        searchingForSth, // where is object xy located?
        wantToBuy,
        wantToTrade,
        hasQuestion, //TODO implement dialog system with choices
        wantsAdvice, //TODO implement dialog system with choices
        justLooking,
        satisfied
    }
    [HideInInspector]
    public state currentState;
    bool coroutineRunning;
    // Start is called before the first frame update
    void Start()
    {
        currentState = state.searchingForSth;// TODO: zukünftig random zuwesísen & haswish nach random sek nach spawn aktivieren
        coroutineRunning = false;
        receivesHelp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!receivesHelp)
        {
            if (hasWish && !coroutineRunning)
            {
                coroutineRunning = true;
                StartCoroutine(reduceSatisfaction());
            }
            else if (!hasWish && coroutineRunning)
            {
                coroutineRunning = false;
                StopCoroutine(reduceSatisfaction());
            }
        }
        else
        {
            if (coroutineRunning)
            {
                coroutineRunning = false;
                StopCoroutine(reduceSatisfaction());
            }
        }
    }

    IEnumerator reduceSatisfaction()
    {
        while (hasWish && !receivesHelp)
        {
            satisfactionLvl -= desatisfactionSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
