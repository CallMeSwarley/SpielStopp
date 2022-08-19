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
        wantToBuy,// TODO nach goto register kann er buy oder traden
        wantToTrade,
        hasQuestion, //TODO implement dialog system with choices
        wantsAdvice, //TODO implement dialog system with choices
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
    // Start is called before the first frame update
    void Start()
    {
        currentState = state.searchingForSth;// TODO: zukünftig random zuwesísen & haswish nach random sek nach spawn aktivieren
        coroutineRunning = false;
        receivesHelp = false;
        registerPos = new Vector3(-13, 1.55f, -2);
        leavingPos = new Vector3(-5, 1.55f, -60);
        goalPos = new Vector3(5, 5, -2);
        agent = GetComponent<NavMeshAgent>();
        myRenderer = GetComponent<Renderer>();
        npc_material = myRenderer.material;
        originalColor = npc_material.GetColor("_Color");
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
        if (currentState == state.leaving)
        {
            agent.destination = leavingPos;
            if (transform.position.x == leavingPos.x && transform.position.z == leavingPos.z)
            {
                Destroy(gameObject);//Laden verlassen
            }
        }
        else if (currentState == state.gotoRegister)
        {
            agent.destination = registerPos;
            if (transform.position.x == registerPos.x && transform.position.z == registerPos.z)
            {
                currentState = state.satisfied;// TODO hier noch kassenwunsch(siehe trade&buy enum) einfügen + zahlvorgang
            }
        }        
        else if (currentState == state.searchingForSth && followPlayer)
        {
            agent.stoppingDistance = 3;
            npc_material.SetColor("_Color", Color.blue);
            agent.destination = transformToFollow.position;
            if (transform.position.x <= goalPos.x + 0.2 && transform.position.x >= goalPos.x - 0.2 &&
                transform.position.y <= goalPos.y + 0.2 && transform.position.y >= goalPos.y - 0.2 &&
                transform.position.z <= goalPos.z + 0.2 && transform.position.z >= goalPos.z - 0.2)
            //bin am zielort(mit radius) angekommen
            {
                followPlayer = false;
                currentState = state.gotoRegister;
                agent.destination = registerPos;
            }
            
        }
        else if (currentState == state.satisfied)
        {
            hasWish = false;
            currentState = state.leaving;
        }
        else// stehenbleiben
        {
            agent.destination = transform.position;//TODO rumgeh verhalten einfügen
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
}
