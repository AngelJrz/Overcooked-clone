using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI recipesCounter;

    public void Start() {
        GameManager.Instance.OnChangeCountDown += Instance_OnChangeCountDown;
        Hide();
    }

    private void Instance_OnChangeCountDown(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGameOver()) {
            recipesCounter.text = DeliveryManager.instance.GetRecipesDelivered().ToString();
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
