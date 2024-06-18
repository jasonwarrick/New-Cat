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

    [Tooltip(" 1: pickup - 2: minigame - 3: environment object")]
    [SerializeField] int type;
    public int Type {
        get { return type; }
        set { type = value; }
    }
}
