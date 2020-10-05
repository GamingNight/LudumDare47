using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMode : MonoBehaviour
{
    public enum Mode
    {
        CONTROLLER, FLUTE, NONE
    }

    private Mode currentMode;
    public PlayerController controller;
    public PlayerFlute flute;
    private Animator animator;
    private bool lockSwitch;
    private Mode goToMode;

    private void Start() {
        currentMode = Mode.CONTROLLER;
        animator = GetComponent<Animator>();
        goToMode = Mode.NONE;
    }

    private void Update() {

        if (lockSwitch)
            return;

        if (Input.GetKeyDown(KeyCode.DownArrow) || goToMode == Mode.FLUTE) {
            currentMode = Mode.FLUTE;
            controller.LockController();
            flute.UnlockFlute();
            animator.SetBool("isFluting", true);
            goToMode = Mode.NONE;
        } else if (Input.GetKeyDown(KeyCode.UpArrow) || goToMode == Mode.CONTROLLER) {
            currentMode = Mode.CONTROLLER;
            flute.LockFlute();
            controller.UnlockController();
            animator.SetBool("isFluting", false);
            goToMode = Mode.NONE;
        }
    }

    public Mode GetCurrentMode() {
        return currentMode;
    }

    public void GoToMode(Mode mode) {
        goToMode = mode;
    }

    public void LockSwitch() {
        lockSwitch = true;
    }

    public void UnlockSwitch() {
        lockSwitch = false;
    }
}
