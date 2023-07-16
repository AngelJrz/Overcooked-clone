using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryRecipeSO : ScriptableObject {
    public KitchenObjectsSO input; 
    public KitchenObjectsSO output;
    public float fryingTimerMax;
}
