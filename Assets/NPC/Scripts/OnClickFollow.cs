//adapted from https://sharpcoderblog.com/blog/npc-follow-player-in-unity-3d
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System;


public class InteractionManager : MonoBehaviour, Interactable
{
    public NPC_master npc_master;
    //Transform that NPC has to follow
    public Transform transformToFollow;
    //NavMesh Agent variable
    NavMeshAgent agent;
    Material npc_material;
    Renderer myRenderer;
    private bool followPlayer = false; // changes when you click on the npc and rechanges if you click again
    Color originalColor;
    bool interactable = true;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        myRenderer = GetComponent<Renderer>();
        npc_material = myRenderer.material;
        originalColor = npc_material.GetColor("_Color");
    }
    // Allows interactables to decide if they are currently interactable
    public bool IsCurrentlyInteractable()
    {
        return interactable;
    }
    // What happens when the player interacts with this interactable?
    public void Interact()
    {
        if (Input.GetMouseButton(0) && followPlayer)
        {
            followPlayer = false;
            npc_master.receivesHelp = false;
            if (npc_master.hasWish && (int)npc_master.currentState == 0 && npc_master.receivesHelp)
            {
                npc_master.receivesHelp = false;
            }
            npc_material.SetColor("_Color", originalColor);//"standby"-mode
        }
        else if (Input.GetMouseButton(0) && !followPlayer)
        {
            followPlayer = true;
            npc_material.SetColor("_Color", Color.blue);//followmode
            if (npc_master.hasWish && (int) npc_master.currentState == 0 && !npc_master.receivesHelp)//wenn er was sucht dann hilft man ihm sonst keine hilfe dadurch. wunscherfüllung muss im npc master passieren
            {
                npc_master.receivesHelp = true;
            }
        }
        else
        {
            npc_material.SetColor("_Color", Color.red);//select mode
        }
    }
    public void endInteract()
    {
        npc_material.SetColor("_Color", originalColor);//exit select mode
    }

    // Update is called once per frame
    void Update()
    {
        //Follow the player
        if (followPlayer) {
            npc_material.SetColor("_Color", Color.blue);
            agent.destination = transformToFollow.position;
        }

    }
}
