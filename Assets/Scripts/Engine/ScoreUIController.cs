using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text countdownText;

    void Update() {

        int score = GameManager.GetInstance().GetScore();
        countdownText.SetText("SCORE: " + score.ToString());

    }
}
