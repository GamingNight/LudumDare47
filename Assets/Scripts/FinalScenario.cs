using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScenario : MonoBehaviour
{
    public enum State
    {
        NONE, INIT, NPC_MOVING, ZOOM_IN, WAIT_NPC, WAIT_DIALOG, DIALOG, WAIT_END_DIALOG, GIVE_FLUTE, WAIT_VICTORY_SOUND, FLUTE_VICTORY_SOUND, WAIT_DIALOG2, DIALOG2, WAIT_END_DIALOG2, ZOOM_OUT, WAIT_ENDING, END
    }

    public GameObject npc;

    private State currentState;
    private float timeSinceLastState;

    void Start() {
        currentState = State.INIT;
        timeSinceLastState = 0;
    }

    // Update is called once per frame
    void Update() {
        timeSinceLastState += Time.deltaTime;

        if (currentState == State.INIT) {
            currentState = Init();
        }
    }

    private State Init() {
        GameObject player = GameController.GetInstance().player;
        player.GetComponent<PlayerController>().LockController();
        player.GetComponent<PlayerMode>().LockSwitch();
        player.transform.localScale = new Vector3(-1, player.transform.localScale.y, player.transform.localScale.z);
        timeSinceLastState = 0;
        float cameraLeftBoundary = Camera.main.GetComponent<CameraMovement>().boundaries.x;
        Camera.main.GetComponent<CameraMovement>().enabled = false;
        Camera.main.transform.position = new Vector3(cameraLeftBoundary, Camera.main.transform.position.y, Camera.main.transform.position.z);
        return State.NPC_MOVING;
    }
}
