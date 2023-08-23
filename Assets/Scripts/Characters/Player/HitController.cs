using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HitController : MonoBehaviour
{
    private bool invincible = false;

    public bool IsInvincible() {
        return invincible;
    }

    public void ProcessHit() {
        invincible = true;
        CameraController.GetInstance().Shake(.7f, .25f);
        transform.DOShakePosition(.5f, .2f, 50).OnComplete(() => invincible = false);
    }
}
