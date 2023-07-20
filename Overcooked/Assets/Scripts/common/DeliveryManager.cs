using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {
    public static DeliveryManager instance { get; private set; }

    [SerializeField] private RecipeListSO RecipeListSO;

    private List<RecipeSO> waitingRecipesOrders;
    private float spawnRecipetimer;
    private float spawnRecipetimerMax = 4f;
    private int waitingRecipesMax = 4;

    private void Awake() {
        instance = this;
        waitingRecipesOrders = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipetimer -= Time.deltaTime;

        if (spawnRecipetimer <= 0) {
            spawnRecipetimer = spawnRecipetimerMax;

            if (waitingRecipesOrders.Count < waitingRecipesMax) {
                RecipeSO waitingRecipe = RecipeListSO.recipes[Random.Range(0, RecipeListSO.recipes.Count)];
                Debug.Log(waitingRecipe.recipeName);
                waitingRecipesOrders.Add(waitingRecipe);
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
                    }
                }

                if (plateContentMatchRecipe) {
                    Debug.Log("Player delivered the correct recipe!");
                    waitingRecipesOrders.RemoveAt(i);
                    return;
                }
            }
        }

        Debug.Log("Player did not deliver the correct recipe");
    }

}