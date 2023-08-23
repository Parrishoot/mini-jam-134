using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ToolAnimationController : MonoBehaviour
{
    private float rotationAmount;

    Sequence sequence;

    private void Start() {
        rotationAmount = transform.eulerAngles.z;
    }

    public void Strike() {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOLocalRotate(new Vector3(0, 0, -70), .1f));
        sequence.Append(transform.DOLocalRotate(new Vector3(0, 0, rotationAmount), .075f)); 

        sequence.SetEase(Ease.InOutCubic);
        sequence.Play();
    }

    public void Shake() {
        transform.DOShakePosition(.075f, strength: .15f, vibrato: 100);
    }

    public bool InProgress() {
        return sequence != null && sequence.IsPlaying();
    }
}
