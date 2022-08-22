//adapted from https://sharpcoderblog.com/blog/npc-follow-player-in-unity-3d
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System;
using UnityEngine.UI;
using Ink.Runtime;
using System.IO;

public class InteractionManagerNPC : MonoBehaviour, Interactable
{
    [SerializeField]
    public NPC_master npc_master;
    Transform transformToFollow;
    public DialogMenu dialogMenu;
    Material npc_material;
    Renderer myRenderer;
    Color originalColor;
    public bool interactable = true;
    public CashRegister cashRegister;
    public Button jaButton;
    Story story;
    [SerializeField]
    GameObject PC;
    RestockPC buyer;

    // Start is called before the first frame update
    void Awake()
    {
        dialogMenu.gameObject.SetActive(false);
        myRenderer = GetComponent<Renderer>();
        npc_material = myRenderer.material;
        buyer = PC.GetComponent<RestockPC>();
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
            cashRegister.npc_master = npc_master;
            cashRegister.interactionManagerNPC = this;
            if (npc_master.currentState == NPC_master.state.justLooking)
            {
                story = createStory("Assets/Dialoge/justLookingDialogue.json");
                startDialog(story);

            }
            else if (npc_master.currentState == NPC_master.state.searchingForSth)
            {
                story = createStory("Assets/Dialoge/helpMeLook.json");
                story.variablesState["lookingFor"] = ProductDataManager.TitleData[npc_master.wantedGameId];
                startDialog(story, npc_master.onClickFollow);
            }
            else if (npc_master.currentState == NPC_master.state.wantToBuy)
            {
                story = createStory("Assets/Dialoge/buyGame.json");
                story.variablesState["GameTitle"] = ProductDataManager.TitleData[npc_master.wantedGameId];
                startDialog(story, cashRegister.sellGame);
                //cashRegister.sellGame();


            }
            else if (npc_master.currentState == NPC_master.state.wantToSell)
            {
                story = createStory("Assets/Dialoge/sellGame.json");
                int randomGameID = UnityEngine.Random.Range(0, ProductDataManager.TitleData.Count - 1);
                story.variablesState["GameToSell"] = ProductDataManager.TitleData[randomGameID];
                int preis = (ProductDataManager.TrendData[randomGameID] + ProductDataManager.RatingData[randomGameID]) * UnityEngine.Random.Range(2, 5);
                story.variablesState["Price"] = preis;
                cashRegister.price = preis;
                startDialog(story, cashRegister.buyGame);//kassenmenü öffnen
 
                
            }
        }
    }

    void GoodEnding() { 
    
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
        if (!dialogMenu.gameObject.active&&story!=null)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0.00001f;
            this.interactable = false;
            dialogMenu.startDialog(this, story, afterDialog);
            dialogMenu.gameObject.SetActive(true);
        }
    }
    public IEnumerator endDialog(bool doAction, Action actionAfterDialog=null)
    {
        //dialogMenu.StartStory();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.2f);
        dialogMenu.gameObject.SetActive(false);
        interactable = true;
        if (doAction && actionAfterDialog != null)
        {
            actionAfterDialog();
        }
        if (!doAction)
        {
            npc_master.currentState = NPC_master.state.leaving;
        }
    }
}
