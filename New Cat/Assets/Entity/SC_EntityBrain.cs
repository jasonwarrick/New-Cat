using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SC_EntityBrain : MonoBehaviour
{
    public static SC_EntityBrain instance;

    [SerializeField] int baseNeedInc;
    [SerializeField] float needTimer;

    float counter = 0f;
    bool stopCounter = false; // Test variable

    public List<CatNeed> catNeeds;

    void Awake() {
        instance = this;
        CatNeed blank = new CatNeed();

        SC_Minigame[] minigames = FindObjectsOfType<SC_Minigame>();

        foreach (CatNeed catNeed in catNeeds) { // Match each cat need to its correct minigame
            foreach (SC_Minigame minigame in minigames) {
                if (minigame.needName == catNeed.name) {
                    catNeed.SetMinigame(minigame.gameObject);
                    continue;
                }
            }
        }
    }

    void Update() {
        counter += Time.deltaTime;

        if (counter >= needTimer & !stopCounter) {
            TriggerFullNeed("left");
            TriggerFullNeed("right");
            TriggerFullNeed("middle");
            stopCounter = true;
        }
    }

    CatNeed FindNeed(string needName) {
        Debug.Log("looking for " + needName);
        foreach (CatNeed catNeed in catNeeds) {
            if (catNeed.name == needName) {
                Debug.Log(catNeed.name + " has been found");
                return catNeed;
            }
        }

        return null;
    }

    public void IncreaseNeed(string needName) { // Increase the priority of a need by the base amount
        CatNeed catNeed = FindNeed(needName);
        if (catNeed == null) { return;}

        if (catNeed.IncreasePriority(baseNeedInc)) { // If the need is full
            TriggerFullNeed(needName);
        }
    }

    public void IncreaseNeed(string needName, int newInc) { // Increase the priority of a need by a specific amount
        CatNeed catNeed = FindNeed(needName);
        if (catNeed == null) { return;}

        catNeed.IncreasePriority(newInc);
    }

    public void TriggerFullNeed(string needName) {
        CatNeed catNeed = FindNeed(needName);
        if (catNeed == null) { return;}
        // Debug.Log(needName + " is full");

        catNeed.minigame.GetComponent<SC_Minigame>().UpdateNeeded(true);
    }

    public void ResetNeed(string needName) {
        Debug.Log("Resetting " + needName);
        CatNeed catNeed = FindNeed(needName);
        if (catNeed == null) { return;}

        catNeed.Reset();
        Debug.Log(catNeed.name + " is reset");
    }
}
