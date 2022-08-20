using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBuilder : MonoBehaviour
{
    [SerializeField]
    Object kid;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")){
            Instantiate(kid);
        }
    }
}
