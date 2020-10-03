using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialog : MonoBehaviour
{
    private TriggerHighlight triggerHighlight;
    private DialogParser parser;
    private bool dialogIsInProgress;

    private int dialogCount;
    private int lineCount;

    private void Start() {
        triggerHighlight = GetComponent<TriggerHighlight>();
        parser = GetComponent<DialogParser>();
        dialogCount = 0;
        lineCount = 0;
    }
    void Update() {
        Text textUI;
        if (Input.GetKeyDown(KeyCode.Space) && triggerHighlight.IsHighLighted() && !dialogIsInProgress) {
            textUI = GameController.GetInstance().LaunchDialogBox(this);
            dialogIsInProgress = true;
        }

        if (dialogIsInProgress) {

        }
    }
}
