using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter
{
    [SerializeField] CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgres;
    public event EventHandler OnCutAnimation;
    public event EventHandler<CuttinCounter_OnCuttingActionEventArgs> OnCuttingAction;
    public class CuttinCounter_OnCuttingActionEventArgs : EventArgs {
        public float progress;
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                if (HasCuttingRecipe(player.GetKitchenObject().GetKitchenObjectSO())) {
                    cuttingProgres = 0;
                    player.GetKitchenObject().SetNewParent(this);
                }
            }
        } else {
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetNewParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        KitchenObject currentKitchenObject = GetKitchenObject();

        if (HasKitchenObject() && HasCuttingRecipe(currentKitchenObject.GetKitchenObjectSO())) {
            cuttingProgres++;

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(currentKitchenObject.GetKitchenObjectSO());
            CuttingActionProgress((float)cuttingProgres / cuttingRecipeSO.cuttingProgresMax);

            if (cuttingProgres >= cuttingRecipeSO.cuttingProgresMax)
            {
                KitchenObjectsSO kitchenObject = GetOutputFromInput(currentKitchenObject.GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(kitchenObject, this);            
            }
        }
    }

    private KitchenObjectsSO GetOutputFromInput(KitchenObjectsSO kitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(kitchenObjectSO);
        if (cuttingRecipeSO != null) {
            return cuttingRecipeSO.output;
        }else { return null; }
    }

    private bool HasCuttingRecipe(KitchenObjectsSO kitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(kitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectsSO inputKitchenObject) {
        foreach (CuttingRecipeSO recipeSO in cuttingRecipeSOArray) {
            if (recipeSO.input == inputKitchenObject) {
                return recipeSO;
            }
        }

        return null;
    }

    private void CuttingActionProgress(float progress) {
        OnCutAnimation?.Invoke(this, EventArgs.Empty);
        OnCuttingAction?.Invoke(this, new CuttinCounter_OnCuttingActionEventArgs() {
            progress = progress
        });
    }

}
