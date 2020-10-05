using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluteListener : MonoBehaviour
{

    public PlayerFlute.Note[] goodSequence;

    private TriggerHighlight triggerHighlight;
    private AbstractFluteListenerScenario scenario;
    private bool noteFound;

    private void Start() {
        triggerHighlight = GetComponentInParent<TriggerHighlight>();
        scenario = GetComponent<AbstractFluteListenerScenario>();
        noteFound = false;
    }

    private void Update() {

        if (triggerHighlight.IsHighLighted() && !noteFound) {
            PlayerFlute.Note[] playerNotes = GameController.GetInstance().player.GetComponentInChildren<PlayerFlute>().ReadMemory();
            noteFound = false;
            int i = 0;
            if (playerNotes.Length >= goodSequence.Length) {
                while (!noteFound && i < playerNotes.Length) {
                    int j = 0;
                    while (j < goodSequence.Length && playerNotes.Length - i >= goodSequence.Length && playerNotes[i + j] == goodSequence[j]) {
                        j++;
                    }
                    if (j == goodSequence.Length) {
                        noteFound = true;
                    } else {
                        i++;
                    }
                }
                if (noteFound) {
                    scenario.InitScenario();
                }
            }
        }
    }
}
