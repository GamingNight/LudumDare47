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
    public Flute flute;

    private void Start() {
        currentMode = Mode.CONTROLLER;
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            currentMode = Mode.FLUTE;
            controller.LockController();
            flute.UnlockFlute();
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            currentMode = Mode.CONTROLLER;
            flute.LockFlute();
            controller.UnlockController();
        }
    }
}
