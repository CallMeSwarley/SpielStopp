//Code adapted from https://www.youtube.com/watch?v=ZYeXmze5gxg
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSatisfaction : MonoBehaviour
{
    public float satisfactionLvl;
    public float maxSatisfaction;

    public GameObject satisfactionBarUI;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        satisfactionLvl = maxSatisfaction;
        slider.value = calcSatisfaction();
        satisfactionBarUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = calcSatisfaction();
        if (satisfactionLvl <= 0)
        {
            Destroy(gameObject);//TODO make him leave the shop & playerrating goes down
        }
        if (satisfactionLvl > maxSatisfaction)
        {
            satisfactionLvl = maxSatisfaction;
        }
    }

    float calcSatisfaction()
    {
        //TODO add more vars to calculate the satisfaction
        //TODO if he has wish, satisfaction goes down fluently
        return satisfactionLvl / maxSatisfaction; //returns value between 0 and 1 vgl. Slider in Unity
    }
}
