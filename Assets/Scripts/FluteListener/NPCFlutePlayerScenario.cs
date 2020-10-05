using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFlutePlayerScenario : AbstractFluteListenerScenario
{
    private enum State
    {
        NOTES_FOUND, SWITCH_DIALOGS, DIALOG, NONE
    }

    public AudioSource victorySound;
    public TriggerDialog victoryDialog;
    public TriggerDialog previousDialog;

    private State currentState;
    private float waitTimeAfterFound;
    private float waitTimeAfterVictorySound;
    private float timeSinceLastWait;

    private void Start() {
        currentState = State.NONE;
        waitTimeAfterFound = 1;
        waitTimeAfterVictorySound = 1;
        timeSinceLastWait = 0;
    }

    public override void InitScenario() {
        currentState = State.NOTES_FOUND;
        GetComponent<AudioSource>().Stop();
        GameController.GetInstance().player.GetComponent<PlayerMode>().GoToMode(PlayerMode.Mode.CONTROLLER);
    }

    protected override void RunScenario() {
        if (currentState == State.NOTES_FOUND) {
            timeSinceLastWait += Time.deltaTime;
            if (timeSinceLastWait >= waitTimeAfterFound) {
                victorySound.Play();
                currentState = State.SWITCH_DIALOGS;
                timeSinceLastWait = 0;
            }
        } else if (currentState == State.SWITCH_DIALOGS) {
            timeSinceLastWait += Time.deltaTime;
            if (timeSinceLastWait >= waitTimeAfterVictorySound) {
                previousDialog.enabled = false;
                victoryDialog.enabled = true;
                currentState = State.DIALOG;
                timeSinceLastWait = 0;
            }
        } else if (currentState == State.DIALOG) {
            if (victoryDialog.enabled) {
                victoryDialog.TriggerDialogAutomatically();
                currentState = State.NONE;
            }
        }
    }
}
