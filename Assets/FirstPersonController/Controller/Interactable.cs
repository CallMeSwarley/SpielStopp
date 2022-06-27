using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    // Allows interactables to decide if they are currently interactable
    bool IsCurrentlyInteractable();

    // Allows the interactable to decide what text to appear when the player looks at it
    //string LookAtText();

    // What happens when the player interacts with this interactable?
    void Interact(); 
    void endInteract();
}
