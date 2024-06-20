using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Interact : MonoBehaviour
{
    [SerializeField] bool isAvailable;
    public bool Available {
        get { return isAvailable; } 
        set { isAvailable = value; }
    }

    [Tooltip(" 1: pickup - 2: minigame - 3: environment object")]
    [Range(1, 3)]
    [SerializeField] int type = 1;
    public int Type {
        get { return type; }
        set { type = value; }
    }
}
