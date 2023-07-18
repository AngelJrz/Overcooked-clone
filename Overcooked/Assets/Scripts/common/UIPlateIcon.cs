using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlateIcon : MonoBehaviour {
    [SerializeField] private Image icon;

    public void SetIcon(KitchenObjectsSO kitchenObjectSO) {
        icon.sprite = kitchenObjectSO.sprite;
    }
}
