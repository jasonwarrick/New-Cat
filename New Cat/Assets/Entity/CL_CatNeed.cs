using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CatNeed
{
    public string name;
    public bool isFull = false;
    public GameObject minigame;
    int priority = 0;

    int maxPriority = 100;

    public void SetMinigame(GameObject inMinigame) {
        minigame = inMinigame;
    }

    public bool IncreasePriority(int inc) {
        priority += inc;
        Debug.Log("Increased priority of " + name);

        if (priority >= maxPriority) {
            Debug.Log(name + " has reached max priority");
            isFull = true;
            return isFull;
        }

        return isFull;
    }

    public void Reset() {
        isFull = false;
        priority = 0;
    }
}
