using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProses {
    [SerializeField] private FryRecipeSO[] fryRecipesArray;

    public event EventHandler<StoveStateChangeArgs> OnStoveStateChange;
    public event EventHandler<IHasProses.OnProgressActionEventArgs> OnProgressAction;

    public class StoveStateChangeArgs : EventArgs {
        public State newState;
    }

    private float fryinTimer;
    private FryRecipeSO fryRecipeSO;
    private State currentState;
    public enum State
    {
        Idle,
        Frying
    }

    private void Start() {
        currentState = State.Idle;
    }

    private void Update() {
        switch (currentState) {
            case State.Idle:
                ChangeStoveVisualState(State.Idle);
                break;
            case State.Frying:
                ChangeStoveVisualState(State.Frying);

                fryinTimer += Time.deltaTime;
                UpdateProgressBar(fryinTimer / fryRecipeSO.fryingTimerMax);
                if (fryinTimer > fryRecipeSO.fryingTimerMax) {
                    ResetTimer();
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(fryRecipeSO.output, this);
                    if (HasFryRecipe(GetKitchenObject().GetKitchenObjectSO())) {
                        fryRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    } else {
                        TurnOffStove();
                    }
                }
                break;
            default:
                break;
        }
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                if (HasFryRecipe(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetNewParent(this);
                    fryRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    currentState = State.Frying;
                }
            }
        } else {
            if (player.HasKitchenObject()) {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                        TurnOffStove();
                    }
                }
            } else {
                GetKitchenObject().SetNewParent(player);
                TurnOffStove();
            }
        }
    }

    private bool HasFryRecipe(KitchenObjectsSO kitchenObjectSO) {
        FryRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(kitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private FryRecipeSO GetFryingRecipeSOWithInput(KitchenObjectsSO inputKitchenObject) {
        foreach (FryRecipeSO recipeSO in fryRecipesArray) {
            if (recipeSO.input == inputKitchenObject) {
                return recipeSO;
            }
        }

        return null;
    }

    private void ChangeStoveVisualState(State state) {
        OnStoveStateChange?.Invoke(this, new StoveStateChangeArgs() {
            newState = state
        });
    }

    private void ResetTimer() {
        fryinTimer = 0f;
    }

    private void TurnOffStove() {
        currentState = State.Idle;
        fryinTimer = 0f;
        UpdateProgressBar(0);
    }

    private void UpdateProgressBar(float newProgress) {
        OnProgressAction?.Invoke(this, new IHasProses.OnProgressActionEventArgs() {
            progress = newProgress
        });
    }
}
