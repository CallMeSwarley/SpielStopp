using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Logic : MonoBehaviour
{
    //lets Unity know that you are in the trigger, initially false
    bool _isInTrigger = false;
    public GameObject door;
    Animator doorAnim;

    private void Start()
    {
        doorAnim = door.GetComponent<Animator>();
        doorAnim.Play("DoorClose");
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && _isInTrigger == false)
        {
            //opening the door
            doorAnim.Play("DoorOpen");
            _isInTrigger = true;
            Debug.Log("player entered trigger");
        }
        else if (collider.gameObject.tag == "Player" && _isInTrigger == true)
        {
            Debug.Log("player already entered");
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        //closing the door
        if (collider.gameObject.tag == "Player" && _isInTrigger == true)
        {
            doorAnim.Play("DoorClose");
            _isInTrigger = false;
            Debug.Log("player has exited");
        }
    }
}
