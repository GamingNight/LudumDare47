using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IntroScenario : MonoBehaviour
{
    public enum State
    {
        NONE, INIT, ZOOM_IN, WAIT_NPC, NPC_POPUP, NPC_MOVING, WAIT_DIALOG, DIALOG, WAIT_END_DIALOG, GIVE_FLUTE, FLUTE_VICTORY_SOUND, WAIT_DIALOG2, DIALOG2, WAIT_END_DIALOG2, ZOOM_OUT, WAIT_ENDING, END
    }

    public GameObject npc;

    private State currentState;
    private float timeSinceLastState;
    private float waitDurToCameraZoomIn;
    private float waitDurToNPCMoving;
    private float waitDurToDialog;
    private float waitDurToEnding;

    void Start() {
        currentState = State.NONE;
        timeSinceLastState = 0;
        waitDurToCameraZoomIn = 1;
        waitDurToNPCMoving = 1;
        waitDurToDialog = 1;
        waitDurToEnding = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            currentState = State.INIT;
            timeSinceLastState = 0;
        }
    }

    void Update() {

        timeSinceLastState += Time.deltaTime;

        if (currentState == State.INIT) {
            currentState = Init();
        } else if (currentState == State.ZOOM_IN) {
            currentState = CameraZoomIn();
        } else if (currentState == State.WAIT_NPC) {
            currentState = WaitTo(State.NPC_POPUP, waitDurToNPCMoving);
        } else if (currentState == State.NPC_POPUP) {
            currentState = NPCPopup();
        } else if (currentState == State.NPC_MOVING) {
            currentState = MoveNPC();
        } else if (currentState == State.WAIT_DIALOG) {
            currentState = WaitTo(State.DIALOG, waitDurToDialog);
        } else if (currentState == State.DIALOG) {
            currentState = LaunchDialog();
        } else if (currentState == State.WAIT_END_DIALOG) {
            currentState = WaitForDialogEnding();
        } else if (currentState == State.GIVE_FLUTE) {
            currentState = GiveFlute();
        } else if (currentState == State.FLUTE_VICTORY_SOUND) {
        } else if (currentState == State.WAIT_DIALOG2) {
        } else if (currentState == State.DIALOG2) {
        } else if (currentState == State.WAIT_END_DIALOG2) {
        } else if (currentState == State.ZOOM_OUT) {
            currentState = CameraZoomOut();
        } else if (currentState == State.WAIT_ENDING) {
            currentState = WaitTo(State.END, waitDurToEnding);
        } else if (currentState == State.END) {
            currentState = Ending();
        }
    }

    private State Init() {
        State res = State.INIT;
        GameObject player = GameController.GetInstance().player;
        player.GetComponent<PlayerController>().LockController();
        player.GetComponent<PlayerMode>().LockSwitch();
        player.transform.localScale = new Vector3(-1, player.transform.localScale.y, player.transform.localScale.z);
        if (timeSinceLastState >= waitDurToCameraZoomIn) {
            res = State.ZOOM_IN;
            timeSinceLastState = 0;
        }
        return res;
    }

    private State CameraZoomIn() {
        Camera.main.GetComponent<Animator>().SetTrigger("zoomIn");
        waitDurToNPCMoving += Camera.main.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        timeSinceLastState = 0;
        return State.WAIT_NPC;

    }

    public State WaitTo(State s, float dur) {
        State res = currentState;
        if (timeSinceLastState >= dur) {
            res = s;
            timeSinceLastState = 0;
        }
        return res;
    }

    private State NPCPopup() {
        float posX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z)).x;
        npc.transform.position = new Vector3(posX - 1, 0, 0);
        npc.SetActive(true);
        timeSinceLastState = 0;
        return State.NPC_MOVING;
    }

    private State MoveNPC() {
        State res = State.NPC_MOVING;
        Vector3 move = new Vector3(1, 0, 0);
        npc.transform.position += move * 2 * Time.deltaTime;
        npc.GetComponent<Animator>().SetBool("isWalking", true);
        AudioSource footstepsSound = null;
        foreach (Transform child in npc.transform) {
            footstepsSound = child.GetComponent<AudioSource>();
            if (footstepsSound != null && !footstepsSound.isPlaying) {
                footstepsSound.Play();
                break;
            }
        }
        if (npc.transform.position.x >= GameController.GetInstance().player.transform.position.x - 0.5) {
            npc.GetComponent<Animator>().SetBool("isWalking", false);
            footstepsSound.Stop();
            timeSinceLastState = 0;
            res = State.WAIT_DIALOG;
        }
        return res;
    }

    private State LaunchDialog() {
        npc.GetComponent<TriggerDialog>().TriggerDialogAutomatically();
        timeSinceLastState = 0;
        return State.WAIT_END_DIALOG;
    }

    private State WaitForDialogEnding() {
        State res = currentState;
        if (!npc.GetComponent<TriggerDialog>().IsDialogInProgress()) {
            GameController.GetInstance().player.GetComponent<PlayerController>().LockController();
            res = State.GIVE_FLUTE;
            timeSinceLastState = 0;
        }
        return res;
    }

    private State GiveFlute() {
        foreach (Transform child in npc.transform) {
            if (child.tag == "Flute")
                child.gameObject.SetActive(false);
        }
        foreach (Transform child in GameController.GetInstance().player.transform) {
            if (child.tag == "Flute")
                child.gameObject.SetActive(true);
        }
        return State.FLUTE_VICTORY_SOUND;
    }

    private State CameraZoomOut() {
        Camera.main.GetComponent<Animator>().SetTrigger("zoomOut");
        waitDurToEnding += Camera.main.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        timeSinceLastState = 0;
        return State.WAIT_ENDING;
    }

    private State Ending() {
        GetComponent<BoxCollider2D>().enabled = false;
        npc.GetComponent<TriggerHighlight>().enabled = true;
        GameController.GetInstance().player.GetComponent<PlayerController>().UnlockController();
        GameController.GetInstance().player.GetComponent<PlayerMode>().enabled = true;
        GameController.GetInstance().player.GetComponent<PlayerMode>().UnlockSwitch();
        return State.NONE;
    }
}
