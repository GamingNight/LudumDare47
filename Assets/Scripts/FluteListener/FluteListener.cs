using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluteListener : MonoBehaviour
{

    public PlayerFlute.Note[] goodSequence;

    private TriggerHighlight triggerHighlight;
    private AbstractFluteListenerScenario scenario;
    private bool sequenceFound;
    private float waitBufferAfterFound;
    private float waitTime;
    private bool scenarioWarned;

    private void Start() {
        triggerHighlight = GetComponentInParent<TriggerHighlight>();
        scenario = GetComponent<AbstractFluteListenerScenario>();
        sequenceFound = false;
        waitBufferAfterFound = 1;
        waitTime = 0;
        scenarioWarned = false;
    }

    private void Update() {
        if (triggerHighlight.IsHighLighted() && !sequenceFound) {
            PlayerFlute.Note[] playerNotes = GameController.GetInstance().player.GetComponentInChildren<PlayerFlute>().ReadMemory();
            sequenceFound = false;
            int i = 0;
            if (playerNotes.Length >= goodSequence.Length) {
                while (!sequenceFound && i < playerNotes.Length) {
                    int j = 0;
                    while (j < goodSequence.Length
                            && playerNotes.Length - i >= goodSequence.Length
                            && (playerNotes[i + j] == goodSequence[j] || goodSequence[j] == PlayerFlute.Note.Whatever)) {
                        j++;
                    }
                    if (j == goodSequence.Length) {
                        sequenceFound = true;
                    } else {
                        i++;
                    }
                }
            }
        }
        if (sequenceFound && !scenarioWarned) {
            waitTime += Time.deltaTime;
            if (waitTime >= waitBufferAfterFound) {
                scenario.InitScenario();
                scenarioWarned = true;
            }
        }
    }
}
