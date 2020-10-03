using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController INSTANCE;

    public static GameController GetInstance() {
        return INSTANCE;
    }


    public float playerLeftestPosition = -15.8f;
    public float playerRightestPosition = 18.7f;
    public GameObject player;
    public GameObject currentLevel;

    private void Awake() {
        if (INSTANCE == null) {
            INSTANCE = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void TriggerNextLevel() {

    }
}
