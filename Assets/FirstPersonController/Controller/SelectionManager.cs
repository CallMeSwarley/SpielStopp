//adapted from https://www.youtube.com/watch?v=_yf5vzZ2sYE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);// von screenmitte
    [SerializeField] private string selectableTag = "Selectable";
    private Transform _selection;
    Color originalColor = Color.blue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_selection!=null)//deselection
        {
            var selectionInteractable = _selection.GetComponent<Interactable>();
            selectionInteractable.endInteract();
            _selection = null;
        }
        var ray = Camera.main.ViewportPointToRay(rayOrigin);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                var selectionInteractable = selection.GetComponent<Interactable>();
                if (selectionInteractable != null && selectionInteractable.IsCurrentlyInteractable())
                {
                    selectionInteractable.Interact();
                }
                _selection = selection;
            }
            
        }
    }
}
