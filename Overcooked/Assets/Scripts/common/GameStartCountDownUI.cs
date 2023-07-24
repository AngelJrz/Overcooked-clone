using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI countDownText;

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
        countDownText.text = Math.Ceiling(GameManager.Instance.GetCountdownTimer()).ToString();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}

