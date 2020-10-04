using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFluteListener : MonoBehaviour
{

    private enum State
    {
        SCANNING_PLAYER_NOTES, NOTES_FOUND, SWITCH_DIALOGS, DIALOG, NONE
    }

    public PlayerFlute.Note[] goodSequence;
    public AudioSource victorySound;
    public TriggerDialog victoryDialog;
    public TriggerDialog previousDialog;

    private TriggerHighlight triggerHighlight;

    private State currentState;
    private float waitTimeAfterFound;
    private float waitTimeAfterVictorySound;
    private float timeSinceLastWait;

    private void Start() {
        triggerHighlight = GetComponentInParent<TriggerHighlight>();
        currentState = State.SCANNING_PLAYER_NOTES;
        waitTimeAfterFound = 1;
        waitTimeAfterVictorySound = 1;
        timeSinceLastWait = 0;
    }

    private void Update() {

        if (triggerHighlight.IsHighLighted()) {
            if (currentState == State.SCANNING_PLAYER_NOTES) {
                PlayerFlute.Note[] playerNotes = GameController.GetInstance().player.GetComponentInChildren<PlayerFlute>().ReadMemory();
                int i = 0;
                bool found = false;
                if (playerNotes.Length >= goodSequence.Length) {
                    while (!found && i < playerNotes.Length) {
                        int j = 0;
                        while (j < goodSequence.Length && playerNotes.Length - i >= goodSequence.Length && playerNotes[i + j] == goodSequence[j]) {
                            j++;
                        }
                        if (j == goodSequence.Length) {
                            found = true;
                        } else {
                            i++;
                        }
                    }
                    if (found) {
                        GetComponent<AudioSource>().Stop();
                        currentState = State.NOTES_FOUND;
                    }
                }
            } else if (currentState == State.NOTES_FOUND) {
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
}
