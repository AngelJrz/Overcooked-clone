using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] CuttingRecipeSO[] cuttingRecipeSOArray;
    
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                if (HasCuttingRecipe(player.GetKitchenObject().GetKitchenObjectSO())) {
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
        if (HasKitchenObject() && HasCuttingRecipe(GetKitchenObject().GetKitchenObjectSO())) {
            KitchenObjectsSO kitchenObject = GetOutputFromInput(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(kitchenObject, this);
        }
    }

    private KitchenObjectsSO GetOutputFromInput(KitchenObjectsSO kitchenObjectSO) {
        foreach (CuttingRecipeSO recipeSO in cuttingRecipeSOArray)
        {
            if (recipeSO.input == kitchenObjectSO)
            {
                return recipeSO.output;
            }
        }

        return null;
    }

    private bool HasCuttingRecipe(KitchenObjectsSO kitchenObjectSO) {
        foreach (CuttingRecipeSO recipeSO in cuttingRecipeSOArray) {
            if (recipeSO.input == kitchenObjectSO) {
                return true;
            }
        }

        return false;
    }

}
