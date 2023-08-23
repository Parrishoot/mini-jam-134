using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BackgroundController : Singleton<BackgroundController>
{
    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private Color redColor;

    [SerializeField]
    private Color blueColor;

    [SerializeField]
    private Color yellowColor;

    public void SetColor(GridColorManager.MapColor mapColor) {

        Color nextColor = redColor;

        switch(mapColor) {

            case(GridColorManager.MapColor.RED):
                nextColor = blueColor;
                break;

            case(GridColorManager.MapColor.BLUE):
                nextColor = yellowColor;
                break;

            case(GridColorManager.MapColor.YELLOW):
                nextColor = redColor;
                break;
        }

        backgroundImage.DOColor(nextColor, 1).SetEase(Ease.InOutCubic);

    }
}
