using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class ButterflyScenario : AbstractFluteListenerScenario
{
    public enum State
    {
        NONE, WAITING_BEFORE_FLY, FLY
    }

    public TriggerDialog previousTriggerDialog;
    public Animator butterflyAnimator;

    private State currentState;
    private float timeSinceLastState;
    private float waitDurBeforeFly;

    private void Start() {
        currentState = State.NONE;
        timeSinceLastState = 0;
        waitDurBeforeFly = 1;
    }

    public override void InitScenario() {
        currentState = State.WAITING_BEFORE_FLY;
    }

    protected override void RunScenario() {

        if (currentState == State.WAITING_BEFORE_FLY) {
            timeSinceLastState += Time.deltaTime;
            if (timeSinceLastState >= waitDurBeforeFly) {
                timeSinceLastState = 0;
                currentState = State.FLY;
            }
        } else if (currentState == State.FLY) {
            butterflyAnimator.SetBool("succeed", true);
            previousTriggerDialog.enabled = false;
            GetComponent<TriggerDialog>().enabled = true;
            timeSinceLastState = 0;
            currentState = State.NONE;
        }
    }
}
