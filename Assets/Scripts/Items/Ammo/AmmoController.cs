using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : Singleton<AmmoController>
{
    [SerializeField]
    private int startingAmmo;

    [SerializeField]
    private int maxAmmo;

    [SerializeField]
    private CounterUIController uiContoller;

    private int currentAmmo;

    public void Reset() {
        uiContoller.Reset();
        currentAmmo = 0;
        for(int i = 0; i < startingAmmo; i++) {
            Reload();
        }
    }

    public void Fire() {
        currentAmmo -= 1;
        uiContoller.RemoveUIElement();
    }

    public void Reload() {
        if(currentAmmo == maxAmmo) {
            return;
        }
        currentAmmo++;
        uiContoller.AddUIElement();
    }

    public bool OutOfAmmo() {
        Debug.Log(currentAmmo);
        return currentAmmo == 0;
    }

    public int GetAmmoAmount() {
        return currentAmmo;
    }
}
