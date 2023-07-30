using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurningUI : MonoBehaviour {
    [SerializeField] private StoveCounter stove;

    private void Start() {
        stove.OnProgressAction += Stove_OnProgressAction;
        Hide();
    }

    private void Stove_OnProgressAction(object sender, IHasProses.OnProgressActionEventArgs e) {
        float burnShowProgressAmount = .5f;
        bool show = stove.IsFried() && e.progress >= burnShowProgressAmount;

        if (show) {
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
