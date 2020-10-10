using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavenScenario : AbstractFluteListenerScenario
{
    private enum State
    {
        NONE, WAIT_BEFORE_FLY, FLY
    }

    private State currentState;
    private float timeSinceLastWait;
    private float waitDureBeforeFly;

    void Start() {
        currentState = State.NONE;
        timeSinceLastWait = 0;
        waitDureBeforeFly = 1;
    }

    public override void InitScenario() {
        currentState = State.WAIT_BEFORE_FLY;
    }

    protected override void RunScenario() {
        if (currentState == State.WAIT_BEFORE_FLY) {
            timeSinceLastWait += Time.deltaTime;
            if (timeSinceLastWait >= waitDureBeforeFly) {
                timeSinceLastWait = 0;
                currentState = State.FLY;
            }
        } else if (currentState == State.FLY) {
            GetComponent<AudioSource>().Stop();
            GetComponent<Animator>().SetBool("succeed", true);
            timeSinceLastWait = 0;
            currentState = State.NONE;
        }
    }
}
