using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletePlateVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plate;
    [SerializeField] private List<KitchenObjects_GameObject> kitchenObjects_GameObjectList;

    [Serializable]
    public struct KitchenObjects_GameObject
    {
        public KitchenObjectsSO kitchenObjects;
        public GameObject visualGameObject;
    }

    public void Start() {
        plate.OnAddIngredient += Plate_OnAddIngredient;
        foreach (KitchenObjects_GameObject item in kitchenObjects_GameObjectList) {
            item.visualGameObject.SetActive(false);
        }
    }

    private void Plate_OnAddIngredient(object sender, PlateKitchenObject.AddIngredientToPlateArgs e) {
        foreach (KitchenObjects_GameObject item in kitchenObjects_GameObjectList) {
            if (item.kitchenObjects == e.objectsSO) {
                item.visualGameObject.SetActive(true);
            }
        }
    }
}
