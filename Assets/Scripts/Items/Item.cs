using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected abstract void Activate();

    protected abstract void Animate();

    public void PickUp() {
        Activate();
        Animate();
    }
}
