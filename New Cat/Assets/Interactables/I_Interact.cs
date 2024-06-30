using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Establishes the basic functionality that all interacts will use
public interface I_Interact
{
    public bool Available { get; set;}
    public int Type { get; set; }

    bool Interact(SC_PlayerInteract playerInteract);
}
