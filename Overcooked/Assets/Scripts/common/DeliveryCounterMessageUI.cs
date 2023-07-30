using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryCounterMessageUI : MonoBehaviour {
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private Color successColor;
    [SerializeField] private Color failureColor;

    [SerializeField] Sprite successIcon;
    [SerializeField] Sprite failureIcon;

    private float messageTime;
    private bool showMessage = false;

    private void Start() {
        DeliveryManager.instance.OnRecipeSuccess += Instance_OnRecipeSuccess;
        DeliveryManager.instance.OnRecipeFailed += Instance_OnRecipeFailed;
        Hide();
    }

    private void Instance_OnRecipeSuccess(object sender, System.EventArgs e) {
        backgroundImage.color = successColor;
        iconImage.sprite = successIcon;
        messageText.text = "Delivery \n Success";

        Show();
    }

    private void Instance_OnRecipeFailed(object sender, System.EventArgs e) {
        backgroundImage.color = failureColor;
        iconImage.sprite = failureIcon;
        messageText.text = "Delivery \n Error";

        Show();
    }

    private void Update() {
        if (showMessage) {
            messageTime -= Time.deltaTime;
            if (messageTime <= 0f) {
                Hide();
                showMessage = false;
            }
        }
    }

    private void Show() {
        gameObject.SetActive(true);
        messageTime = 1.5f;
        showMessage = true;
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
