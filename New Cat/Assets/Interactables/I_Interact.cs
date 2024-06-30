using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Establishes the basic functionality that all interacts will use
public interface I_Interact
{
    public bool Available { get; set;}
    [Tooltip(" 1: pickup - 2: minigame - 3: environment object")]
    public int Type { get; set; }

    bool Interact();
}
