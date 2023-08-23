using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class GameUIManager : MonoBehaviour
{
    
    [SerializeField]
    private float fadeOutTime = .5f;

    [SerializeField]
    Image panelImage;

    [SerializeField]
    TMP_Text headerText;

    [SerializeField]
    TMP_Text scoreText;

    [SerializeField]
    TMP_Text pressSpaceToPlayText;

    [SerializeField]
    private List<GameObject> gameUIObjects;

    public void BeginGame() {

        Sequence s = DOTween.Sequence();

        s.Append(panelImage.DOFade(0f, fadeOutTime));
        s.Join(headerText.DOFade(0f, fadeOutTime));
        s.Join(scoreText.DOFade(0f, fadeOutTime));
        s.Join(pressSpaceToPlayText.DOFade(0f, fadeOutTime));

        foreach(GameObject gameUIObject in gameUIObjects) {
            gameUIObject.SetActive(true);
        }
    }

    public void EndGame(int score) {

        headerText.SetText("GAME OVER");
        scoreText.SetText("SCORE: " + score.ToString());

        Sequence s = DOTween.Sequence();

        s.Append(panelImage.DOFade(.75f, fadeOutTime));
        s.Join(headerText.DOFade(1f, fadeOutTime));
        s.Join(scoreText.DOFade(1f, fadeOutTime));
        s.Join(pressSpaceToPlayText.DOFade(1f, fadeOutTime));

        foreach(GameObject gameUIObject in gameUIObjects) {
            gameUIObject.SetActive(false);
        }
    }

}
