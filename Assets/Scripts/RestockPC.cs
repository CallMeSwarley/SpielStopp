using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestockPC : MonoBehaviour, Interactable
{
    public void endInteract()
    {
        
        
    }

    public void Interact()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Boop");
        }
    }

    public bool IsCurrentlyInteractable()
    {
        return true;
    }

}
