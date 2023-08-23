using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class ConsumableItem : Item
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    protected override void Animate()
    {
        audioSource.Play();

        Vector3 positionToGoTo = GameObject.FindGameObjectWithTag("ItemShowTransform").transform.position;

        Sequence s = DOTween.Sequence();
        s.Append(transform.DOMove(positionToGoTo, .5f).SetEase(Ease.InOutCubic))
         .Join(transform.DORotate(new Vector3(0, 0, 720), .5f, RotateMode.FastBeyond360))
         .Join(transform.DOScale(new Vector3(1.5f, 1.5f, 1), .5f).SetEase(Ease.InOutCubic))
         .Append(spriteRenderer.DOFade(0f, 2f));
        
        s.Play().OnComplete(() => {
            Destroy(gameObject);
        });
    }
}
