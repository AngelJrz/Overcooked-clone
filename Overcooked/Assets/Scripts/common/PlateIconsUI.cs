using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconsUI : MonoBehaviour {
    [SerializeField] private PlateKitchenObject plate;
    [SerializeField] private Transform iconTemplate;


    public void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }

    public void Start() {
        plate.OnAddIngredient += Plate_OnAddIngredient;
    }

    private void Plate_OnAddIngredient(object sender, PlateKitchenObject.AddIngredientToPlateArgs e) {
        Transform newIconItem = Instantiate(iconTemplate, transform);


        newIconItem.gameObject.SetActive(true);
        newIconItem.GetComponent<UIPlateIcon>().SetIcon(e.objectsSO);
    }
}
