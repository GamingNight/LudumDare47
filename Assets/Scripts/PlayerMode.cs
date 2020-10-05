using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMode : MonoBehaviour
{
    public enum Mode
    {
        CONTROLLER, FLUTE
    }

    private Mode currentMode;
    public PlayerController controller;
    public PlayerFlute flute;
    private Animator animator;

    private void Start() {
        currentMode = Mode.CONTROLLER;
        animator = GetComponent<Animator>();
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            currentMode = Mode.FLUTE;
            controller.LockController();
            flute.UnlockFlute();
            animator.SetBool("isFluting", true);
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            currentMode = Mode.CONTROLLER;
            flute.LockFlute();
            controller.UnlockController();
            animator.SetBool("isFluting", false);
        }
    }

    public Mode GetCurrentMode() {
        return currentMode;
    }
}
