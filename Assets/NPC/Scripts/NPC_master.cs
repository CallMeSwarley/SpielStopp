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
        arriving,
        searchingForSth, // where is object xy located?
        wantToBuy,
        wantToSell,
        justLooking, // default wert-> wenn man ihn dann anspricht sagte er "just looking"
        satisfied, // wenn er hier ist macht er sich dann los den laden zu verlassen & man bekommt punkte auf kundenzufriedenheit
        leaving,
        gotoRegister,
        Idle
    }
    public enum type { 
        Seller,
        Know,
        Quiz
    }
    type kundenType;
    NavMeshAgent agent;
    Material npc_material;
    Renderer myRenderer;
    Color originalColor;
    private bool followPlayer = false; // changes when you click on the npc and rechanges if you click again
    public state currentState;
    bool coroutineRunning;
    bool wantedGameNotHere = true;
    [SerializeField]
    public int wantedGameId;
    [SerializeField]
    public GameObject[] Shelves = new GameObject[8];
    void Awake()
    {
        satisfactionLvl = maxSatisfaction;
        currentState = state.arriving;// TODO: zukünftig random zuweisen & haswish nach random sek nach spawn aktivieren
        coroutineRunning = false;
        receivesHelp = false;
        registerPos = new Vector3(-43, 1.53731f, -14);
        leavingPos = new Vector3(-13, 2, -60);
        goalPos = new Vector3(-25, 5, -8);//erstes regal oben rechts
        agent = GetComponent<NavMeshAgent>();
        myRenderer = GetComponent<Renderer>();
        npc_material = myRenderer.material;
        originalColor = npc_material.GetColor("_Color");
        wantedGameId = selectWishedGame();
        kundenType = SelectType();
        if (myRenderer.transform.position.x == 42) {
            desatisfactionSpeed = 0;
        }


    }

    type SelectType(){
        return type.Know;
        int random = UnityEngine.Random.Range(0, 5);
        if (random <= 1) {
            return type.Know;
        } else if (random < 4)
        {
            return type.Quiz;
        }
        else {
            return type.Seller;
        }
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
        for (int i = 0; i < ProductDataManager.RatingData.Count-1; i++)
        {
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
        for (int i = 0; i < ProductDataManager.TrendData.Count-1; i++)
        {
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
        for (int i = 0; i < ProductDataManager.Genre1Data.Count-1; i++)
        {
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
        hasWish = (currentState == state.justLooking || currentState == state.leaving || currentState == state.gotoRegister || currentState==state.arriving) ? false : true;
        
        
        switch (currentState) {
            case state.arriving:
                enterStore();
                break;
            case state.justLooking:
                Wander();
                break;
            case state.Idle:
                npc_material.SetColor("_Color", Color.blue);
                break;
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
        if (transform.position.x < -12 && transform.position.z <= -59)
        {
            Destroy(gameObject);//Laden verlassen
        }
    }

    private void leadMe() {
        agent.stoppingDistance = 3;
        npc_material.SetColor("_Color", Color.blue);
        agent.destination = transformToFollow.position;
        if (transform.position.x <= goalPos.x + 1 && transform.position.x >= goalPos.x - 1 &&
            transform.position.y <= goalPos.y + 0.2 && transform.position.y >= goalPos.y - 0.2 &&
            transform.position.z <= goalPos.z + 1 && transform.position.z >= goalPos.z - 1)
        {
            followPlayer = false;
            currentState = state.wantToBuy;
            agent.destination = registerPos;
        }
    }

    private void goToRegister()  
    {
        agent.destination = registerPos;
    }

    private void enterStore()
    {
        agent.destination = new Vector3(-41, 2, -15);
        if (transform.position.x <= -40 && transform.position.z <= -14 && transform.position.y < 9)
        {
            switch (kundenType) {
                case type.Know:
                    
                    int random = UnityEngine.Random.Range(0, 4);
                    switch (random)
                    {
                        case 0:
                            goalPos = new Vector3(-51.887f, 5.113f, -12.748f);
                            break;
                        case 1:
                            goalPos = new Vector3(-24.636f, 5.113f, -16.347f);
                            break;
                        case 2:
                            goalPos = new Vector3(-33 - 906f, 1.514f, -7.506f);
                            break;
                        case 3:
                            goalPos = new Vector3(-52.997f, 1.514f, -11.028f);
                            break;
                        default:
                            goalPos = new Vector3(-28, 4.235f, -9.24f);
                            break;
                    }
                    currentState = state.justLooking;
                    break;
                case type.Quiz:
                    
                    currentState = state.searchingForSth;
                    break;
                case type.Seller:
                    
                    currentState = state.wantToSell;
                    break;
                    
            } 
        }
    }

    private void Wander() {
        Debug.Log("I want " + ProductDataManager.TitleData[wantedGameId]);
        agent.destination = goalPos;
        foreach(GameObject Check in Shelves){
            Debug.Log("Checked a shelf");
            Debug.Log(Check.GetComponent<ShelvesBehavior>().takeGame(wantedGameId));
            if (Check.GetComponent<ShelvesBehavior>().takeGame(wantedGameId)){
                Debug.Log("FoundYa");
                wantedGameNotHere = false;
                break;
            }
        }
        StartCoroutine(Pause());

    }
    IEnumerator Pause() {
        int random = UnityEngine.Random.Range(0, 21);
        yield return new WaitForSeconds(10 + random);
        if (wantedGameNotHere)
        {
            currentState = state.leaving;
        }
        else
        {
            currentState = state.wantToBuy;
            goToRegister();
        }
    }
}
