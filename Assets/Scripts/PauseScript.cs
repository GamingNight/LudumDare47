using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{

    public GameObject background;
    public GameObject pausePanel;


    private bool pauseIsActive;

    void Start() {
        pauseIsActive = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            background.SetActive(!background.activeSelf);
            pausePanel.SetActive(!pausePanel.activeSelf);
            pauseIsActive = !pauseIsActive;
        }

        if (Input.GetKeyDown(KeyCode.Q) && pauseIsActive) {
            Application.Quit();
        }
    }
}
