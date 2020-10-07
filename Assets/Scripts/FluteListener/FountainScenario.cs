using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainScenario : AbstractFluteListenerScenario
{
    public enum State
    {
        NONE, WAIT_BEFORE_WATER, WATER
    }

    public AudioSource fountainSignalDistress;
    public GameObject fountainParticleObj;
    public TriggerDialog previousDialog;

    private State currentState;
    private float timeSinceLastState;
    private float waitDurBeforeWater;

    void Start() {
        currentState = State.NONE;
        timeSinceLastState = 0;
        waitDurBeforeWater = 1;
    }
    public override void InitScenario() {
        currentState = State.WAIT_BEFORE_WATER;
    }

    protected override void RunScenario() {

        if (currentState == State.WAIT_BEFORE_WATER) {
            timeSinceLastState += Time.deltaTime;
            if (timeSinceLastState >= waitDurBeforeWater) {
                timeSinceLastState = 0;
                currentState = State.WATER;
            }
        } else if (currentState == State.WATER) {
            fountainSignalDistress.Stop();
            fountainParticleObj.SetActive(true);
            previousDialog.enabled = false;
            GetComponent<TriggerDialog>().enabled = true;
            timeSinceLastState = 0;
            currentState = State.NONE;
        }
    }
}
