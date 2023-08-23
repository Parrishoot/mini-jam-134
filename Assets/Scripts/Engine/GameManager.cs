using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    private int timePerRound = 60;

    [SerializeField]
    private int killEnemyPoints = 100;

    [SerializeField]
    private int timeMultiplier = 10;

    [SerializeField]
    private int findTreasurePoints = 1000;

    [SerializeField]
    private GameUIManager gameUIManager;

    [SerializeField]
    private AudioSource audioSource;

    private int score = 0;

    private bool countDown = false;

    private bool gameOver = true;

    private float currentTime;

    public void Update() {
        CountDown();
        CheckPlayAgain();
    }



    private void CheckPlayAgain()
    {
        if(gameOver && Input.GetKeyDown(KeyCode.Space)) {
            gameUIManager.BeginGame();

            HealthCounter.GetInstance().Reset();
            AmmoController.GetInstance().Reset();

            score = 0;

            gameOver = false;

            EndRound();
        }
    }

    private void CountDown() {

        if(!countDown) {
            return;
        }

        currentTime -= Time.deltaTime;

        if(currentTime <= 0) {
            EndGame();
        }
    }

    public void EndGame() {
        PauseGame();
        gameOver = true;
        gameUIManager.EndGame(score);
    }

    public void PauseGame() {
        foreach(GridMover gridMover in FindObjectsOfType<GridMover>()) {
            gridMover.SetActive(false);
        }

        countDown = false;
    }

    public void StartGame() {
        foreach(GridMover gridMover in FindObjectsOfType<GridMover>()) {
            gridMover.SetActive(true);
        }

        countDown = true;
    }

    public void BeginRound() {
        currentTime = timePerRound;
        StartGame();
    }

    public void EndRound() {
        PauseGame();

        foreach(EnemyBoatController crab in GameObject.FindObjectsOfType<EnemyBoatController>()) {
            crab.Die();
        }

        TickController.GetInstance().ResetActions();
        LevelCreator.GetInstance().CreateNewLevel();
    }

    public int GetRemainingTime() {
        return (int) Mathf.Ceil(currentTime); 
    }

    public void CollectChest() {
        score += findTreasurePoints;
        score += (GetRemainingTime() * timeMultiplier);
    }

    public void KillEnemy() {
        score += killEnemyPoints;
        audioSource.Play();
    }

    public int GetScore() {
        return score;
    }

    public bool IsPaused() {
        return countDown == false;
    }
}
