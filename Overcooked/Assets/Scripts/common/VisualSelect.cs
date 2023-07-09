using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualSelect : MonoBehaviour
{
    [SerializeField] private BaseCounter clearCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        if (e.selectedCounter == clearCounter) {
            foreach (GameObject visualGameObject in visualGameObjectArray)
            {
                visualGameObject.SetActive(true);
            }
        } else {
            foreach (GameObject visualGameObject in visualGameObjectArray) {
                visualGameObject.SetActive(false);
            }
        }
    }
}
