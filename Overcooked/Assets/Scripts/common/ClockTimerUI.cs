using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockTimerUI : MonoBehaviour {
    [SerializeField] private Image clock;

    public void Start() {
        GameManager.Instance.OnChangeCountDown += Instance_OnChangeCountDown;
        Hide();
    }

    private void Instance_OnChangeCountDown(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsPlaying()) {
            Show();
        } else {
            Hide();
        }
    }

    public void Update() {
        clock.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
