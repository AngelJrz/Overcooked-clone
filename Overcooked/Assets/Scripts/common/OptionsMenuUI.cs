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

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;

    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI effectsVolumeText;

    [SerializeField] private TextMeshProUGUI moveUpButtonText;
    [SerializeField] private TextMeshProUGUI moveDownButtonText;
    [SerializeField] private TextMeshProUGUI moveLeftButtonText;
    [SerializeField] private TextMeshProUGUI moveRightButtonText;
    [SerializeField] private TextMeshProUGUI interactButtonText;
    [SerializeField] private TextMeshProUGUI interactAlternateButtonText;

    [SerializeField] private Transform remapKeybindingImage;

    private void Awake() {
        instance = this;

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

        moveUpButton.onClick.AddListener(() => {
            UpdateKeyBinding(GameInput.Bindings.MoveUp);
        });

    }

    private void Start() {
        GameManager.Instance.OnUnPause += Instance_OnUnPause;
        UpdateKeyBindings();
        UpdateVisuals();
        HideRemapBackground();
        Hide();
    }

    private void Instance_OnUnPause(object sender, System.EventArgs e) {
        Hide();
    }

    public void UpdateVisuals() {
        musicVolumeText.text = "Music volume: " + Mathf.Round(MusicManager.Instance.GetMusicVolume() * 10f);
        effectsVolumeText.text = "Effects volume: " + Mathf.Round(SoundManager.instance.GetVolume() * 10f);
    }

    public void UpdateKeyBindings(){
        moveUpButtonText.text = GameInput.Instance.GetKeyBinding(GameInput.Bindings.MoveUp);
        moveDownButtonText.text = GameInput.Instance.GetKeyBinding(GameInput.Bindings.MoveDown);
        moveLeftButtonText.text = GameInput.Instance.GetKeyBinding(GameInput.Bindings.MoveLeft);
        moveRightButtonText.text = GameInput.Instance.GetKeyBinding(GameInput.Bindings.MoveRight);
        interactButtonText.text = GameInput.Instance.GetKeyBinding(GameInput.Bindings.Interact);
        interactAlternateButtonText.text = GameInput.Instance.GetKeyBinding(GameInput.Bindings.InteractAlternate);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void ShowRemapBackground() {
        remapKeybindingImage.gameObject.SetActive(true);
    }

    public void HideRemapBackground() {
        remapKeybindingImage.gameObject.SetActive(false);
    }

    public void UpdateKeyBinding(GameInput.Bindings key) {
        ShowRemapBackground();
        GameInput.Instance.SetNewKeyBinding(key, () => {
            HideRemapBackground();
            UpdateKeyBindings();
        });
    }
}
