using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CatNeed
{
    public string name;
    GameObject minigame;
    int priority = 0;

    int maxPriority = 100;

    public void SetMinigame(GameObject inMinigame) {
        minigame = inMinigame;
    }

    public void IncreasePriority(int inc) {
        priority += inc;

        if (priority >= maxPriority) {
            Debug.Log(name + " has reached max priority");
        }
    }
}
