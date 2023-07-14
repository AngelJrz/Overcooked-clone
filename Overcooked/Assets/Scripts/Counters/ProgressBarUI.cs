using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttinCounter;
    [SerializeField] private Image barImage;
    private Canvas canvas;

    public void Start() {
        cuttinCounter.OnCuttingAction += CuttinCounter_OnCuttingAction;
        Hide();
    }

    private void CuttinCounter_OnCuttingAction(object sender, CuttingCounter.CuttinCounter_OnCuttingActionEventArgs e) {
        barImage.fillAmount = e.progress;

        if (barImage.fillAmount == 1 || barImage.fillAmount == 0) {
            Hide();
           
        } else {
            Show();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
