using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownUIController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text countdownText;

    void Update() {

        int remainingTime = GameManager.GetInstance().GetRemainingTime();
        countdownText.SetText("TIME: " + remainingTime.ToString());

    }
}
