using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject counter;
    [SerializeField] private Image barImage;

    private IHasProses counterWithProcess;

    public void Start() {
        counterWithProcess = counter.GetComponent<IHasProses>();
        counterWithProcess.OnProgressAction += CuttinCounter_OnProgressAction;
        Hide();
    }

    private void CuttinCounter_OnProgressAction(object sender, IHasProses.OnProgressActionEventArgs e) {
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
