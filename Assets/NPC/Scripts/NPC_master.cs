using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_master : MonoBehaviour //for all the stats & bahaviour of the npc
{

    public Transform transformToFollow;
    // Satisfaction
    public float satisfactionLvl;
    public float maxSatisfaction;
    [Range(0.1f, 1.0f)]
    public float desatisfactionSpeed = 1f;

    // wish
    public bool hasWish;//wunscherfüllung wir hier in jeweiligen methoden passieren
    public bool receivesHelp;
    public Vector3 goalPos;// wenn er was sucht dann hier die zielcoords angeben
    public Vector3 leavingPos;
    public Vector3 registerPos;
    [HideInInspector]
    public enum state 
    {
        searchingForSth, // where is object xy located?
        wantToBuy,
        wantToSell,
        justLooking, // default wert-> wenn man ihn dann anspricht sagte er "just looking"
        satisfied, // wenn er hier ist macht er sich dann los den laden zu verlassen & man bekommt punkte auf kundenzufriedenheit
        leaving,
        gotoRegister
    }
    NavMeshAgent agent;
    Material npc_material;
    Renderer myRenderer;
    Color originalColor;
    private bool followPlayer = false; // changes when you click on the npc and rechanges if you click again
    public state currentState;
    bool coroutineRunning;
    bool wantedGameNotHere = false;
    // Start is called before the first frame update
    
    public int wantedGameId;
    void Start()
    {
        currentState = state.justLooking;// TODO: zukünftig random zuweisen & haswish nach random sek nach spawn aktivieren
        coroutineRunning = false;
        receivesHelp = false;
        registerPos = new Vector3(-42, 1.55f, -15);
        leavingPos = new Vector3(-42, 1.55f, -30);
        goalPos = new Vector3(5, 5, -2);
        agent = GetComponent<NavMeshAgent>();
        myRenderer = GetComponent<Renderer>();
        npc_material = myRenderer.material;
        originalColor = npc_material.GetColor("_Color");
        wantedGameId = selectWishedGame();
    }

    int selectWishedGame()
    {

        //Generiert Wunschliste vom Kunden entweder basierend auf Rating, Trend oder Genre der Spiele. 
        int random = UnityEngine.Random.Range(0, 3);
        switch (random)
        {
            //Nachteil: Nimmt immer erstes spiel in der liste bei jeder der methoden. TODO: Ein spiel aus Pool mit richtigen Bewertungen aussuchen? 
            case 0:
                return selectFromRating();

            case 1:
                return selectFromTrend();
            case 2:
                return selectFromGenre();
        }
        return 0;
    }

    int selectFromRating()
    {
        List<int> PotentialGames = new List<int>();
        //Kunde will nur spiele mit Rating 4-5
        for (int i = 0; i < ProductDataManager.RatingData.Count; i++)
        {
            Debug.Log(i);
            if (ProductDataManager.RatingData[i] >= 4)
            {
                PotentialGames.Add(i);
            }
        }
        if (PotentialGames.Count > 0)
        {
            int GameId = UnityEngine.Random.Range(0, PotentialGames.Count);
            return GameId;
        }
        return 0;
    }

    int selectFromTrend()
    {
        List<int> PotentialGames = new List<int>();
        //Kunde will nur spiele mit Trend 4-5
        for (int i = 0; i < ProductDataManager.TrendData.Count; i++)
        {
            Debug.Log(i);
            if (ProductDataManager.TrendData[i] >= 4)
            {
                PotentialGames.Add(i);
            }
        }
        if (PotentialGames.Count > 0)
        {
            int GameId = UnityEngine.Random.Range(0, PotentialGames.Count);
            return GameId;
        }
        return 0;
    }

    int selectFromGenre()
    {
        Genre wanted = GenreGiver.giveGenre();

        List<int> PotentialGames = new List<int>();
        //Kunde will nur spiele mit Trend 4-5
        for (int i = 0; i < ProductDataManager.Genre1Data.Count; i++)
        {
            Debug.Log(i);
            if (ProductDataManager.Genre1Data[i] == wanted || ProductDataManager.Genre2Data[i] == wanted)
            {
                PotentialGames.Add(i);
            }
        }
        if (PotentialGames.Count > 0)
        {
            int GameId = UnityEngine.Random.Range(0, PotentialGames.Count);
            return GameId;
        }
        return 0;
    }

    public void onClickFollow()
    {
        if (followPlayer)// er folgt dem spieler bereits
        {
            followPlayer = false;
            receivesHelp = false;
            npc_material.SetColor("_Color", originalColor);//"standby"-mode
        }
        else if (!followPlayer)// folgt spieler noch nicht
        {
            followPlayer = true;
            receivesHelp = true;
            npc_material.SetColor("_Color", Color.blue);//followmode
        }
    }

    // Update is called once per frame
    void Update()
    {
        agent.stoppingDistance = 0;
        hasWish = (currentState == state.justLooking || currentState == state.leaving || currentState == state.gotoRegister) ? false : true;

        switch (currentState) {
            case state.leaving:
                leaveStore();
                break;
            case state.gotoRegister:
                goToRegister();
                break;
            case state.searchingForSth:
                if (followPlayer == true) {
                    leadMe();
                }
                else
                {
                    agent.destination = transform.position;
                }
                break;
            case state.satisfied:
                hasWish = false;
                currentState = state.leaving;
                break;
            case state.wantToSell:
                goToRegister();
                break;
            case state.wantToBuy:
                goToRegister();
                break;
            default:
                agent.destination = transform.position;
                break;
        }       
               
        if (!receivesHelp)//zufriedenheitsanzeige regeln
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

    private void leaveStore() {
        agent.destination = leavingPos;
        if (transform.position.x == leavingPos.x && transform.position.z == leavingPos.z)
        {
            Destroy(gameObject);//Laden verlassen
        }
    }

    private void leadMe() {
        agent.stoppingDistance = 3;
        npc_material.SetColor("_Color", Color.blue);
        agent.destination = transformToFollow.position;
        if (transform.position.x <= goalPos.x + 0.2 && transform.position.x >= goalPos.x - 0.2 &&//TODO coordinaten vom gesuchten game einfügen
            transform.position.y <= goalPos.y + 0.2 && transform.position.y >= goalPos.y - 0.2 &&
            transform.position.z <= goalPos.z + 0.2 && transform.position.z >= goalPos.z - 0.2)
        {
            followPlayer = false;
            currentState = state.gotoRegister;
            agent.destination = registerPos;
        }
    }

    private void goToRegister()
    {
        agent.destination = registerPos;
        //if (transform.position.x == registerPos.x && transform.position.z == registerPos.z)
        //{
        //    currentState = state.wantToBuy;
        //}
    }
}
