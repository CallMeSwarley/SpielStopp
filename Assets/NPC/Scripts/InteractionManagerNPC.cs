//adapted from https://sharpcoderblog.com/blog/npc-follow-player-in-unity-3d
using UnityEngine;
using UnityEditor;
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
    public bool interactable = true;
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
                startDialog("Assets/Dialoge/justLookingDialogue.json");
            }
            else if (npc_master.currentState == NPC_master.state.searchingForSth)
            {
                startDialog("Assets/Dialoge/justLookingDialogue.json", npc_master.onClickFollow);
            }
            else if(npc_master.currentState == NPC_master.state.wantToBuy)
            {
                Debug.Log("Lemme Buy!");//TODO start buy menu
            }
        }
    }
    public void endInteract()
    {
        npc_material.SetColor("_Color", originalColor);//exit select mode
    }
    void startDialog(String path,Action afterDialog=null)//path where the storyjson is
    {
        if (!dialogMenu.gameObject.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0.00001f;
            interactable = false;
            dialogMenu.startDialog(this, path, afterDialog);
            dialogMenu.gameObject.SetActive(true);
        }
    }
    public void endDialog(Action actionAfterDialog=null)
    {
        dialogMenu.gameObject.SetActive(false);
        dialogMenu.StartStory();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        interactable = true;
        if (actionAfterDialog != null)
        {
            actionAfterDialog();
        }
    }
}
