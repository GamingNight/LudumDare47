﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialog : MonoBehaviour
{
    private TriggerHighlight triggerHighlight;
    private DialogParser parser;
    private Text textUI;
    private Image endOfLineCursorImg;

    private bool dialogIsInProgress;
    private List<string> currentDialog;
    private int dialogId;
    private int lineId;
    private int cursorId;

    private bool showLineProgressively;
    private float timeSinceLastChar;

    private bool waitingForNextLine;
    private float endOfLineCursorFlashSpeed;
    private float timeSinceLastCursorFlash;

    private AudioSource voix;

    private bool goTriggerAutomatically;

    private void Start() {
        triggerHighlight = GetComponent<TriggerHighlight>();
        parser = GetComponent<DialogParser>();
        voix = GetComponent<AudioSource>();
        dialogId = 0;
        lineId = 0;
        cursorId = 0;
        currentDialog = null;
        textUI = null;
        endOfLineCursorImg = null;
        showLineProgressively = false;
        timeSinceLastChar = 1 / GameController.GetInstance().dialogSpeed;
        waitingForNextLine = false;
        endOfLineCursorFlashSpeed = 2;
        timeSinceLastCursorFlash = 0;
        goTriggerAutomatically = false;
    }
    void Update() {

        bool manualTrigger = Input.GetKeyDown(KeyCode.Space) && triggerHighlight != null && triggerHighlight.IsHighLighted();
        bool automaticTrigger = goTriggerAutomatically;
        bool playerIsAvailable = GameController.GetInstance().player.GetComponent<PlayerMode>().GetCurrentMode() == PlayerMode.Mode.CONTROLLER;
        if ((manualTrigger || automaticTrigger) && playerIsAvailable && !dialogIsInProgress) {
            textUI = GameController.GetInstance().LaunchDialogBox(this);
            endOfLineCursorImg = textUI.GetComponentInChildren<Image>();
            dialogIsInProgress = true;
            currentDialog = parser.GetDialog(dialogId);
            goTriggerAutomatically = false;
        }

        if (dialogIsInProgress) {
            if (lineId == 0 || Input.GetKeyDown(KeyCode.Space)) {
                if (showLineProgressively && Input.GetKeyDown(KeyCode.Space)) {
                    textUI.text = currentDialog[lineId];
                    ReachEndOfLine();
                } else if (lineId < currentDialog.Count) {
                    showLineProgressively = true;
                    StopWaitingForNextLine();
                } else {
                    dialogId = Mathf.Min(parser.GetDialogCount() - 1, dialogId + 1);
                    textUI.text = "";
                    GameController.GetInstance().QuitDialogBox();
                    dialogIsInProgress = false;
                    currentDialog = null;
                    lineId = 0;
                    StopWaitingForNextLine();
                }
            }

            if (showLineProgressively) {
                if (timeSinceLastChar >= 1 / GameController.GetInstance().dialogSpeed) {
                    cursorId++;
                    textUI.text = currentDialog[lineId].Substring(0, cursorId);
                    timeSinceLastChar = 0;
                    if (cursorId == currentDialog[lineId].Length) {
                        ReachEndOfLine();
                    }
                }
                timeSinceLastChar += Time.deltaTime;
            }

            if (waitingForNextLine) {
                timeSinceLastCursorFlash += Time.deltaTime;
                if (timeSinceLastCursorFlash >= 1 / endOfLineCursorFlashSpeed) {
                    endOfLineCursorImg.enabled = !endOfLineCursorImg.enabled;
                    timeSinceLastCursorFlash = 0;
                }
            }
        }

        if (showLineProgressively && !voix.isPlaying) {
            voix.Play();
        } else if (!showLineProgressively && voix.isPlaying) {
            voix.Stop();
        }

    }

    private void ReachEndOfLine() {
        showLineProgressively = false;
        lineId++;
        cursorId = 0;
        endOfLineCursorImg.enabled = true;
        waitingForNextLine = true;
    }

    private void StopWaitingForNextLine() {
        waitingForNextLine = false;
        endOfLineCursorImg.enabled = false;
        timeSinceLastCursorFlash = 0;
    }

    public void TriggerDialogAutomatically() {
        goTriggerAutomatically = true;
    }

    public bool IsDialogInProgress() {
        return dialogIsInProgress;
    }
}
