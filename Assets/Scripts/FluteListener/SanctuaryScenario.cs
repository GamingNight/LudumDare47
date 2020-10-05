using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SanctuaryScenario : AbstractFluteListenerScenario
{
    private enum State
    {
        WAITING_FOR_ANIMATION, PLAYER_COLOR_CHANGE, END, NONE
    }

    public Animator[] toTrigger;

    private State currentState;
    private Color playerInitColor;
    private float waitingAnimationDuration;
    private float colorChangeDuration;
    private float timeSinceLastState;

    void Start() {
        currentState = State.NONE;
        waitingAnimationDuration = 1.5f;
        colorChangeDuration = 1;
        timeSinceLastState = 0;
        playerInitColor = GameController.GetInstance().player.GetComponent<SpriteRenderer>().color;
    }

    public override void InitScenario() {
        foreach (Animator animator in toTrigger) {
            animator.SetBool("succeed", true);
        }
        GameController.GetInstance().player.GetComponent<PlayerMode>().GoToMode(PlayerMode.Mode.CONTROLLER);
        GameController.GetInstance().player.GetComponent<PlayerController>().LockController();
        GetComponent<AudioSource>().Stop();
        currentState = State.WAITING_FOR_ANIMATION;
        timeSinceLastState = 0;
    }

    protected override void RunScenario() {

        if (currentState == State.WAITING_FOR_ANIMATION) {
            timeSinceLastState += Time.deltaTime;
            if (timeSinceLastState >= waitingAnimationDuration) {
                currentState = State.PLAYER_COLOR_CHANGE;
                timeSinceLastState = 0;
            }
        } else if (currentState == State.PLAYER_COLOR_CHANGE) {
            SpriteRenderer playerSpriteRenderer = GameController.GetInstance().player.GetComponent<SpriteRenderer>();
            playerSpriteRenderer.color = Color.Lerp(playerInitColor, Color.white, timeSinceLastState / colorChangeDuration);
            timeSinceLastState += Time.deltaTime;
            if (timeSinceLastState >= colorChangeDuration) {
                currentState = State.END;
                timeSinceLastState = 0;
            }
        } else if (currentState == State.END) {
            GameController.GetInstance().player.GetComponent<PlayerController>().UnlockController();
            GameController.GetInstance().ActivateNextLevelTrigger();
            currentState = State.NONE;
        }
    }
}
