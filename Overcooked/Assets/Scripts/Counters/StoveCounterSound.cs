using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour {
    [SerializeField] StoveCounter stoveCounter;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStoveStateChange += StoveCounter_OnStoveStateChange;
    }

    private void StoveCounter_OnStoveStateChange(object sender, StoveCounter.StoveStateChangeArgs e) {
        bool playSound = e.newState == StoveCounter.State.Frying;
        if (playSound) {
            audioSource.Play();
        } else {
            audioSource.Pause();
        }
    }
}
