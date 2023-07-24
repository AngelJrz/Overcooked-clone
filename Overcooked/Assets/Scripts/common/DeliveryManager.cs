using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager instance { get; private set; }

    [SerializeField] private RecipeListSO RecipeListSO;

    private List<RecipeSO> waitingRecipesOrders;
    private float spawnRecipetimer = 4f;
    private float spawnRecipetimerMax = 4f;
    private int waitingRecipesMax = 4;

    public int recipesDelivered = 0;

    private void Awake() {
        instance = this;
        waitingRecipesOrders = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipetimer -= Time.deltaTime;

        if (spawnRecipetimer <= 0) {
            spawnRecipetimer = spawnRecipetimerMax;

            if (waitingRecipesOrders.Count < waitingRecipesMax) {
                RecipeSO waitingRecipe = RecipeListSO.recipes[UnityEngine.Random.Range(0, RecipeListSO.recipes.Count)];
                Debug.Log(waitingRecipe.recipeName);
                waitingRecipesOrders.Add(waitingRecipe);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plate) {
        for (int i = 0; i < waitingRecipesOrders.Count; i++) {
            RecipeSO waitingOrder = waitingRecipesOrders[i];

            if (plate.GetKitchenObjectsList().Count == waitingOrder.ingredientsSO.Count) {
                bool plateContentMatchRecipe = true;
                foreach (KitchenObjectsSO kitchenObject in waitingOrder.ingredientsSO)
                {
                    bool ingredientFound = false;

                    foreach (KitchenObjectsSO plateKitchenObject in plate.GetKitchenObjectsList()) {
                        if (plateKitchenObject == kitchenObject) {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound) {
                        plateContentMatchRecipe = false;
                        break;
                    }
                }

                if (plateContentMatchRecipe) {
                    Debug.Log("Player delivered the correct recipe!");
                    waitingRecipesOrders.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    recipesDelivered++;
                    return;
                }
            }
        }

        Debug.Log("Player did not deliver the correct recipe");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipesSOList() {
        return waitingRecipesOrders;
    }

    public int GetRecipesDelivered() {
        return recipesDelivered;
    }
}