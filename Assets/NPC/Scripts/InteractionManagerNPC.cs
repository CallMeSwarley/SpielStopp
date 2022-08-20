//adapted from https://sharpcoderblog.com/blog/npc-follow-player-in-unity-3d
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System;


public class InteractionManagerNPC : MonoBehaviour, Interactable
{
    public NPC_master npc_master;
    public Transform transformToFollow;
    public DialogMenu dialogMenu;
    Material npc_material;
    Renderer myRenderer;
    Color originalColor;
    bool interactable = true;
    // Start is called before the first frame update
    void Start()
    {
        dialogMenu.gameObject.SetActive(false);
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
        npc_material.SetColor("_Color", Color.red);//select mode
        if (Input.GetMouseButtonDown(0))
        {   
            if(npc_master.currentState == NPC_master.state.justLooking)
            {
                //dialogMenu.inkJSONAsset = (TextAsset)GameObject.Find("Assets/Dialoge/justLookingDialogue.ink");
                if (dialogMenu.gameObject.activeSelf)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Time.timeScale = 1f;
                    dialogMenu.gameObject.SetActive(false);
                    interactable = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    Time.timeScale = 0.00001f;
                    interactable = false;
                    dialogMenu.gameObject.SetActive(true);
                }                
            }
            else if (npc_master.currentState == NPC_master.state.searchingForSth)
            {
                npc_master.onClickFollow();
            }
        }
    }
    public void endInteract()
    {
        npc_material.SetColor("_Color", originalColor);//exit select mode
    }

}
