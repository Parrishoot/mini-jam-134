using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : Singleton<CameraController>
{
    public void Shake(float strength, float time) {
        transform.DOShakePosition(time, strength, 50);
    }
}
