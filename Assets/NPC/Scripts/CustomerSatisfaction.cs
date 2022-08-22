//Code adapted from https://www.youtube.com/watch?v=ZYeXmze5gxg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSatisfaction : MonoBehaviour
{
    public NPC_master npc_master;

    public GameObject satisfactionBarUI;
    public Slider slider;
    public Image fill;

    float satisfactionLvl;
    float maxSatisfaction;
    private bool receivedSatisfactionpoints = false;
    // Start is called before the first frame update
    void Start()
    {
        maxSatisfaction= npc_master.maxSatisfaction;
        slider.value = calcSatisfaction();
        satisfactionBarUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float val= calcSatisfaction();
        slider.value = val;
        fill.color = Color.Lerp(new Color(1, 0, 0, 1),new Color(0, 1, 0, 1), (float)slider.value);//green->red

        if (npc_master.satisfactionLvl <= 0 && npc_master.currentState != NPC_master.state.leaving)
        {
            npc_master.currentState = NPC_master.state.leaving;
            Display_UI.zufriedenheitsWert -= 10f;
        }
        if (npc_master.satisfactionLvl > maxSatisfaction)
        {
            npc_master.satisfactionLvl = maxSatisfaction;
        }
        if (npc_master.currentState == NPC_master.state.satisfied && !receivedSatisfactionpoints)
        {
            Debug.Log("Receives points");
            Display_UI.zufriedenheitsWert += (int)(10f*val);
            receivedSatisfactionpoints = true;
            npc_master.currentState = NPC_master.state.leaving;
        }
    }

    float calcSatisfaction()
    {
        return npc_master.satisfactionLvl / maxSatisfaction; //returns value between 0 and 1 vgl. Slider in Unity
    }
}
