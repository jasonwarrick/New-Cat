using System.Collections.Generic;
using UnityEngine;

public class SC_EntityBrain : MonoBehaviour
{
    public static SC_EntityBrain instance;

    [SerializeField] int baseNeedInc;
    [SerializeField] int maxStartPriority;
    [SerializeField] float needTimer;
    [SerializeField] float needTickRate;

    float needCounter = 0f;
    bool stopCounter = false; // Test variable

    public List<CatNeed> catNeeds;
    SC_CatBrain catBrain;

    void Awake() {
        instance = this;
        catBrain = FindObjectOfType<SC_CatBrain>();

        SC_Minigame[] minigames = FindObjectsOfType<SC_Minigame>();

        foreach (CatNeed catNeed in catNeeds) { // Match each cat need to its correct minigame
            foreach (SC_Minigame minigame in minigames) {
                if (minigame.needName == catNeed.name) {
                    int priority = Random.Range(0, maxStartPriority);
                    catNeed.SetMinigame(minigame.gameObject);
                    catNeed.SetPriority(priority);
                    continue;
                }
            }
        }
    }

    void Update() {
        needCounter += Time.deltaTime;

        if (needCounter >= needTickRate && !stopCounter) { // Every tick increase all needs by the same base amount
            IncreaseAllNeed();
            needCounter = 0f;
        }
    }

    CatNeed FindNeed(string needName) {
        foreach (CatNeed catNeed in catNeeds) {
            if (catNeed.name == needName) {
                return catNeed;
            }
        }

        return null;
    }

    void IncreaseAllNeed() { // Increase all needs by the base amound
        foreach (CatNeed catNeed in catNeeds) {
            IncreaseNeed(catNeed.name);
        }
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
        catBrain.MoveToMinigame(catNeed.minigame.transform);
    }

    public void ResetNeed(string needName) {
        Debug.Log("Resetting " + needName);
        CatNeed catNeed = FindNeed(needName);
        if (catNeed == null) { return;}

        catNeed.Reset();
        Debug.Log(catNeed.name + " is reset");
    }

    public void FailedMinigame() {
        Debug.Log("Entity knows minigame was failed");
    }
}
