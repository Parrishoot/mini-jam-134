using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCounter : Singleton<HealthCounter>
{
    [SerializeField]
    private int totalLives = 5;

    private int currentLives;

    [SerializeField]
    private CounterUIController uIController; 

    [SerializeField]
    private AudioSource audioSource; 

    public void Reset() {
        currentLives = totalLives;
        uIController.Reset();
        for(int i = 0; i < totalLives; i++) {
            uIController.AddUIElement();
        }
    }

    public void LoseLife() {
        currentLives--;
        uIController.RemoveUIElement();

        audioSource.Play();

        if(currentLives == 0) {
            GameManager.GetInstance().EndGame();
        }
    }

    public int GetLives() {
        return currentLives;
    }
}
