using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; set; }

    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    public event EventHandler OnChangeCountDown;

    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 20f;
    private float countDownEnd = 0f;

    private State state;

    private void Awake() {
        state = State.WaitingToStart;
        Instance = this;
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < countDownEnd) {
                    state = State.CountdownToStart;
                    OnChangeCountDown?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < countDownEnd) {
                    gamePlayingTimer = gamePlayingTimerMax;
                    state = State.GamePlaying;
                    OnChangeCountDown?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < countDownEnd) {
                    state = State.GameOver;
                    OnChangeCountDown?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive() {
        return state == State.CountdownToStart;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public float GetCountdownTimer() {
        return countdownToStartTimer;
    }

    public bool IsPlaying() {
        return state == State.GamePlaying;
    }

    public float GetGamePlayingTimerNormalized() {
        return Math.Abs(gamePlayingTimer / gamePlayingTimerMax);
    }
}

public static class Loader {
    public enum GameScene {
        GameScene,
        LoadingScene,
        MainMenuScene
    }

    private static GameScene targetScene;

    public static void Load(GameScene targetScene) {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(GameScene.LoadingScene.ToString());
    }

    public static void LoaderCallBack() {
        SceneManager.LoadScene(targetScene.ToString());
    }
}