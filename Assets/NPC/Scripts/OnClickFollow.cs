//adapted from https://sharpcoderblog.com/blog/npc-follow-player-in-unity-3d
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;


public class OnClickFollow : MonoBehaviour
{
    //Transform that NPC has to follow
    public Transform transformToFollow;
    //NavMesh Agent variable
    NavMeshAgent agent;
    Material npc_material;
    Renderer myRenderer;
    private bool followPlayer = false; // changes when you click on the npc and rechanges if you click again
    Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        myRenderer = GetComponent<Renderer>();
        npc_material = myRenderer.material;
        originalColor = npc_material.GetColor("_Color");
    }

    void OnMouseOver()
    {
        npc_material.SetColor("_Color", Color.red);//select mode
        if (Input.GetMouseButton(0) && followPlayer)
        {
            followPlayer = false;
            npc_material.SetColor("_Color", originalColor);//"standby"-mode
        }
        else if (Input.GetMouseButton(0) && !followPlayer)
        {
            followPlayer = true;
            npc_material.SetColor("_Color", Color.blue);//followmode
        }

        Debug.Log("Mouse over NPC");
    }
    void OnMouseExit() {
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
        else
        {
            npc_material.SetColor("_Color", originalColor);
        }
    }
}
