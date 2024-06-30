using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Pickup : MonoBehaviour, I_Interact
{
    [SerializeField] bool available = false;
    [Range(1, 3)]
    [SerializeField] int type = 1;
    
    public bool Available {
        get { return available; }
        set { available = value; }
    }

    public int Type {
        get { return type; }
        set { type = value; }
    }

    public bool Interact() {
        throw new System.NotImplementedException();
    }
}
