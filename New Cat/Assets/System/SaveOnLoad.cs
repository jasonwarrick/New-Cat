using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOnLoad : MonoBehaviour
{
    [SerializeField] bool rootObject = false;
    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(transform.parent.gameObject);
    }

    void Awake() {
        SaveOnLoad[] opposingObjects = FindObjectsOfType<SaveOnLoad>();
        foreach (SaveOnLoad opposingObject in opposingObjects) {
            if (opposingObject.transform.parent.gameObject.name.Equals(this.transform.parent.gameObject.name) && !rootObject && opposingObject.transform.parent.gameObject != this.transform.parent.gameObject) {
                Debug.Log(this.transform.parent.gameObject);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
