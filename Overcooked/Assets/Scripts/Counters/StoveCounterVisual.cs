using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject burningParticlesGameObject;

    void Start()
    {
        stoveCounter.OnStoveStateChange += StoveCounter_OnStoveStateChange;
    }

    private void StoveCounter_OnStoveStateChange(object sender, StoveCounter.StoveStateChangeArgs e) {
        switch (e.newState) {
            case StoveCounter.State.Frying:
                stoveOnGameObject.SetActive(true);
                burningParticlesGameObject.SetActive(true);
                break;
            case StoveCounter.State.Idle:
                stoveOnGameObject.SetActive(false);
                burningParticlesGameObject.SetActive(false);
                break;
        }
    }
}
