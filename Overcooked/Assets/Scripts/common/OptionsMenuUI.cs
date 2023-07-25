using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour {

    public static OptionsMenuUI instance { get; private set; }

    [SerializeField] private Button musicVolumeButton;
    [SerializeField] private Button effectsVolumeButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI effectsVolumeText;

    private void Awake() {

        musicVolumeButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeMusicVolume();
            UpdateVisuals();
        });

        effectsVolumeButton.onClick.AddListener(() => {
            SoundManager.instance.ChangeVolume();
            UpdateVisuals();
        });

        exitButton.onClick.AddListener(() => {
            Hide();
        });

        instance = this;
        GameManager.Instance.OnUnPause += Instance_OnUnPause;
        UpdateVisuals();
        Hide();
    }

    private void Instance_OnUnPause(object sender, System.EventArgs e) {
        Hide();
    }

    public void UpdateVisuals() {
        musicVolumeText.text = "Music volume: " + Mathf.Round(MusicManager.Instance.GetMusicVolume() * 10f);
        effectsVolumeText.text = "Effects volume: " + Mathf.Round(SoundManager.instance.GetVolume() * 10f);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
