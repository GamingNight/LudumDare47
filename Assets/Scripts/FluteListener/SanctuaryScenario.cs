using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanctuaryScenario : AbstractFluteListenerScenario
{
    private enum State
    {
        WAITING_FOR_ANIMATION, PLAYER_COLOR_CHANGE, END, NONE
    }

    public Animator[] toTrigger;

    private State currentState;

    void Start() {
        currentState = State.NONE;
    }

    public override void InitScenario() {
        foreach (Animator animator in toTrigger) {
            animator.SetBool("succeed", true);
        }
        currentState = State.WAITING_FOR_ANIMATION;
    }

    protected override void RunScenario() {
        //Changer la couleur du player en blanc
    }

}
