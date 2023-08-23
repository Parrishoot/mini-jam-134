using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DespawnableUIController : MonoBehaviour
{
    [SerializeField]
    private Image image;

    public void Despawn() {

        Sequence s = DOTween.Sequence();

        s.Append(transform.DOMoveY(transform.position.y - 30, .25f));
        s.Join(image.DOFade(0, .25f));
        s.Join(transform.DORotate(new Vector3(0, 0, -45), .25f));

        s.Play().OnComplete(() => Destroy(gameObject));
    }
}
