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
    public Display_UI display_UI;
    public Image fill;

    float satisfactionLvl;
    float maxSatisfaction;

    // Start is called before the first frame update
    void Start()
    {
        maxSatisfaction= npc_master.maxSatisfaction;
        satisfactionLvl = npc_master.maxSatisfaction;
        slider.value = calcSatisfaction();
        satisfactionBarUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = calcSatisfaction();
        fill.color = Color.Lerp(new Color(1, 0, 0, 1),new Color(0, 1, 0, 1), (float)slider.value);

        if (npc_master.satisfactionLvl <= 0)
        {
            Destroy(gameObject);//TODO make him leave the shop 
            display_UI.zufriedenheitsWert -= 10f;
        }
        if (npc_master.satisfactionLvl > maxSatisfaction)
        {
            npc_master.satisfactionLvl = maxSatisfaction;
        }
    }

    float calcSatisfaction()
    {
        //TODO add more vars to calculate the satisfaction
        //TODO if he has wish, satisfaction goes down fluently
        return npc_master.satisfactionLvl / maxSatisfaction; //returns value between 0 and 1 vgl. Slider in Unity
    }
}
