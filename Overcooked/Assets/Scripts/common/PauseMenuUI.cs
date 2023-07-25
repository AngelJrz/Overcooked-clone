using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour {
    [SerializeField] Button resumeButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button optionsButton;

    private void Start () {
        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePause();
        });

        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.GameScene.MainMenuScene);
        });

        optionsButton.onClick.AddListener(() => {
            OptionsMenuUI.instance.Show();
        });

        GameManager.Instance.OnPause += Instance_OnPause;
        GameManager.Instance.OnUnPause += Instance_OnUnPause;

        Hide();
    }

    private void Instance_OnUnPause(object sender, System.EventArgs e) {
        Hide();
    }

    private void Instance_OnPause(object sender, System.EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
