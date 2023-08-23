using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TItleUIController : MonoBehaviour
{
    void Start() {

        transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 1f).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);

    }
}
