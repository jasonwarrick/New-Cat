using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Door : MonoBehaviour, I_Interact
{
    [Tooltip("Positive values open to the right")]
    [SerializeField] float openAmt;

    bool isOpen = false;

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

    public bool Interact(SC_PlayerInteract playerInteract) {
        if (!available) { return available;}

        OpenDoor();
        
        return available;
    }

    void OpenDoor() {
        float currentOpenAmt;
        if (!isOpen) {
            currentOpenAmt = openAmt;
        } else {
            currentOpenAmt = -openAmt;
        }

        transform.Rotate(new Vector3(0f, currentOpenAmt, 0f));
        isOpen = !isOpen;
    }
}
