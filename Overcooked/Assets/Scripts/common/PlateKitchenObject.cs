using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    [SerializeField] private List<KitchenObjectsSO> allowedKitchenObject;

    private List<KitchenObjectsSO> kitchenObjectSOList;

    public event EventHandler<AddIngredientToPlateArgs> OnAddIngredient;
    public class AddIngredientToPlateArgs : EventArgs {
        public KitchenObjectsSO objectsSO;
    }

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectsSO>();
    }

    public bool TryAddIngredient(KitchenObjectsSO kitchenObjectsSO) {
        if (!allowedKitchenObject.Contains(kitchenObjectsSO)) {
            return false;
        }

        if (kitchenObjectSOList.Contains(kitchenObjectsSO)) {
            return false;
        }
        else {
            kitchenObjectSOList.Add(kitchenObjectsSO);
            OnAddIngredient?.Invoke(this, new AddIngredientToPlateArgs() {
                objectsSO = kitchenObjectsSO
            });
            return true;
        }
    }
}
