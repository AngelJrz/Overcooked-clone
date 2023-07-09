using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualContainerCounter : MonoBehaviour {
    private Animator animator;
    [SerializeField] ContainerCounter containerCounter;
    private const string OPEN_CLOSE = "OpenClose";

    public void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
