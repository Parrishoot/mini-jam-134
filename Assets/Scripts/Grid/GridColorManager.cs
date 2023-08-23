using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridColorManager : MonoBehaviour
{
    public enum MapColor {
        RED,
        BLUE,
        YELLOW
    }

    [SerializeField]
    private Sprite blueSprite;

    [SerializeField]
    private Sprite redSprite;

    [SerializeField]
    private Sprite yellowSprite;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public void SetColor(MapColor mapColor) {
        switch(mapColor) {

            case MapColor.BLUE:
                spriteRenderer.sprite = blueSprite;
                break;

            case MapColor.RED:
                spriteRenderer.sprite = redSprite;
                break;

            case MapColor.YELLOW:
                spriteRenderer.sprite = yellowSprite;
                break;
        }
    }
}
