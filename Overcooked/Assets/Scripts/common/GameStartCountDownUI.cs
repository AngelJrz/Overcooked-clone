using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI countDownText;

    private Animator animator;
    private int previousCountDownNumber;
    private string POP_UP_TRIGGER = "numberPopUp";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        GameManager.Instance.OnChangeCountDown += Instance_OnChangeCountDown;
        Hide();
    }

    private void Instance_OnChangeCountDown(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsCountdownToStartActive()) {
            Show();
        } else {
            Hide();
        }
    }

    private void Update() {
        int countDown = Mathf.CeilToInt(GameManager.Instance.GetCountdownTimer());
        countDownText.text = countDown.ToString();

        if (countDown != previousCountDownNumber) {
            previousCountDownNumber = countDown;
            animator.SetTrigger(POP_UP_TRIGGER);
            SoundManager.instance.PlayCoundownSound();
        }
    }
    
    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}

