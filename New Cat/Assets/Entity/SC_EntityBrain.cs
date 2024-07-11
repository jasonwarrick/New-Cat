using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_EntityBrain : MonoBehaviour
{
    [SerializeField] int baseNeedInc;

    public List<CatNeed> catNeeds;

    void Awake() {
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

    CatNeed FindNeed(string needName) {
        foreach (CatNeed catNeed in catNeeds) {
            if (catNeed.name == needName) {
                return catNeed;
            }
        }

        return null;
    }

    public void IncreaseNeed(string needName) { // Increase the priority of a need by the base amount
        CatNeed catNeed = FindNeed(needName);
        if (catNeed == null) { return;}

        catNeed.IncreasePriority(baseNeedInc);
    }

    void IncreaseNeed(string needName, int newInc) { // Increase the priority of a need by a specific amount
        CatNeed catNeed = FindNeed(needName);
        if (catNeed == null) { return;}

        catNeed.IncreasePriority(newInc);
    }
}
