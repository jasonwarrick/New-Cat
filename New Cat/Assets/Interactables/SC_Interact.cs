using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Interact : MonoBehaviour
{
    [SerializeField] bool isAvailable;
    public bool Available {
        get { return isAvailable; }   // get method
        set { isAvailable = value; }  // set method
    }
}
