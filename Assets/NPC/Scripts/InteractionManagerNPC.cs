//adapted from https://sharpcoderblog.com/blog/npc-follow-player-in-unity-3d
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System;
using Ink.Runtime;


public class InteractionManagerNPC : MonoBehaviour, Interactable
{
    public NPC_master npc_master;
    public Transform transformToFollow;
    public DialogMenu dialogMenu;
    Material npc_material;
    Renderer myRenderer;
    Color originalColor;
    public bool interactable = true;
    public ProductDataManager productDataManager;
    Story story;
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
                story = createStory("Assets/Dialoge/justLookingDialogue.json");
                startDialog(story);
               
            }
            else if (npc_master.currentState == NPC_master.state.searchingForSth)
            {
                story = createStory("Assets/Dialoge/helpMeLook.json");
                story.variablesState["lookingFor"] = productDataManager.getTitle(npc_master.wantedGameId);
                startDialog(story, npc_master.onClickFollow);
            }
            else if(npc_master.currentState == NPC_master.state.wantToBuy)
            {
                story = createStory("Assets/Dialoge/buyGame.json");
                story.variablesState["GameTitle"] = productDataManager.getTitle(npc_master.wantedGameId);
                startDialog(story);//kassenmenü öffnen
            }
            else if (npc_master.currentState == NPC_master.state.wantToSell)
            {
                story = createStory("Assets/Dialoge/sellGame.json");
                story.variablesState["GameToSell"] = productDataManager.getTitle(npc_master.wantedGameId);
                startDialog(story);//kassenmenü öffnen
            }
        }
    }
    Story createStory(String path)
    {
        TextAsset textAsset = (TextAsset)AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset));
        if (textAsset!=null)
        {
            return new Story(textAsset.text);
        }
        else
        {
            Debug.Log("Couldnt find story at this path: "+path);
            return null;
        }        
    }
    public void endInteract()
    {
        npc_material.SetColor("_Color", originalColor);//exit select mode
    }
    void startDialog(Story story,Action afterDialog=null)//path where the storyjson is
    {
        if (!dialogMenu.gameObject.activeSelf&&story!=null)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0.00001f;
            interactable = false;
            dialogMenu.startDialog(this, story, afterDialog);
            dialogMenu.gameObject.SetActive(true);
        }
    }
    public void endDialog(bool doAction, Action actionAfterDialog=null)
    {
        dialogMenu.gameObject.SetActive(false);
        //dialogMenu.StartStory();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        interactable = true;
        if (doAction && actionAfterDialog != null)
        {
            actionAfterDialog();
        }
    }
}
