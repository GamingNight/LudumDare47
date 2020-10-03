using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialog : MonoBehaviour
{
    private TriggerHighlight triggerHighlight;
    private DialogParser parser;
    private bool dialogIsInProgress;
    private Text textUI;
    private int dialogId;
    private int lineId;
    private List<string> currentDialog;

    private void Start() {
        triggerHighlight = GetComponent<TriggerHighlight>();
        parser = GetComponent<DialogParser>();
        dialogId = 0;
        lineId = 0;
        currentDialog = null;
        textUI = null;
    }
    void Update() {

        if (Input.GetKeyDown(KeyCode.Space) && triggerHighlight.IsHighLighted() && !dialogIsInProgress) {
            textUI = GameController.GetInstance().LaunchDialogBox(this);
            dialogIsInProgress = true;
            currentDialog = parser.GetDialog(dialogId);
        }

        if (dialogIsInProgress) {
            if (lineId == 0 || Input.GetKeyDown(KeyCode.Space)) {
                if (lineId == currentDialog.Count) {
                    dialogId = Mathf.Min(parser.GetDialogCount() - 1, dialogId + 1); ;
                    GameController.GetInstance().QuitDialogBox();
                    dialogIsInProgress = false;
                    currentDialog = null;
                    lineId = 0;
                } else {
                    textUI.text = currentDialog[lineId];
                    lineId++;
                }
            }
        }
    }
}
