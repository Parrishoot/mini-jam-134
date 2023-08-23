using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructableOccupant : GridOccupant
{
    
    [SerializeField]
    private int hitsRequired = 3;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private AudioSource audioSource;

    private int currentHits = 0;

    private bool destroyed = false;

    public void Hit() {

        currentHits++;

        audioSource.Play();

        if(destroyed) {
            return;
        }

        if(currentHits >= hitsRequired) {
            destroyed = true;            

            DoDestroy();

            transform.DOShakePosition(.25f, strength: .2f, vibrato: 100).OnComplete(() => {
                Destroy(gameObject);
            });
            spriteRenderer.DOFade(0f, .25f);
        }
        else {
            transform.DOShakePosition(.075f, strength: .2f, vibrato: 100);
        }
    }

    public bool IsDestroyed() {
        return destroyed;
    }

    protected virtual void DoDestroy() {

    }
}
