using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualCuttingCounter : MonoBehaviour {
    private Animator animator;
    [SerializeField] CuttingCounter cuttingCounter;
    private const string CUT = "Cut";

    public void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        cuttingCounter.OnCutAnimation += CuttingCounter_OnCutAnimation;
    }

    private void CuttingCounter_OnCutAnimation(object sender, System.EventArgs e) {
        animator.SetTrigger(CUT);
    }
}
