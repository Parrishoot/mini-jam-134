using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterUIController : MonoBehaviour
{
    [SerializeField]
    GameObject uiPrefab;

    Queue<GameObject> uiElements = new Queue<GameObject>();

    public void Reset() {
        while(uiElements.Count > 0) {
            RemoveUIElement();
        }
        uiElements = new Queue<GameObject>();
    }

    public void AddUIElement() {
        uiElements.Enqueue(Instantiate(uiPrefab, transform.position, Quaternion.identity, transform));
    }

    public void RemoveUIElement() {
        if(uiElements.Count > 0) {
            uiElements.Dequeue().GetComponent<DespawnableUIController>().Despawn();
        }
    }
}
