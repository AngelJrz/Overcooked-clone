using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleOrderUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform newIconTemplate;

    public void Start() {
        newIconTemplate.gameObject.SetActive(false);
    }

    public void SetOrderInfo(RecipeSO recipe) {
        recipeName.text = recipe.recipeName;
        
        foreach (KitchenObjectsSO ingredient in recipe.ingredientsSO) {
            Transform icon = Instantiate(newIconTemplate, iconContainer);
            icon.GetComponent<Image>().sprite = ingredient.sprite;
        }
    }
}
