using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public Image switchLevelFadeImage;
    public GameObject dialogPanel;
    public float dialogSpeed = 2;
    public GameObject nextLevelTrigger;

    private void Awake() {
        if (INSTANCE == null) {
            INSTANCE = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void LaunchLevelSwitching(LevelTrigger.Type type) {
        GameObject otherLevel = null;
        float playerPosX = 0;
        if (type == LevelTrigger.Type.NEXT) {
            otherLevel = currentLevel.GetComponent<LevelData>().nextLevel;
            playerPosX = playerLeftestPosition;
        } else if (type == LevelTrigger.Type.PREVIOUS) {
            otherLevel = currentLevel.GetComponent<LevelData>().previousLevel;
            playerPosX = playerRightestPosition;
        }
        if (otherLevel == null)
            return;

        StartCoroutine(LevelSwitchingCoroutine(1, otherLevel, playerPosX));
    }

    private void ResetPlayer() {
        player.transform.position = new Vector3(-9, 0, 0);
        player.GetComponent<SpriteRenderer>().color = new Color(65f / 255f, 63f / 255f, 60f / 255f);
        foreach (Transform child in player.transform) {
            if (child.tag == "Flute")
                child.gameObject.SetActive(false);
        }
    }

    private void SwitchLevels(GameObject newLevel, float playerPosX) {
        //Switch levels
        if (currentLevel.GetComponent<LevelData>().id == 5) {
            SceneManager.LoadScene("Main 2");
        }
        if (newLevel.GetComponent<LevelData>().id == 4)
            nextLevelTrigger.SetActive(false);
        newLevel.SetActive(true);
        currentLevel.SetActive(false);
        currentLevel = newLevel;
        //Shift player to its new position
        player.transform.position = new Vector3(playerPosX, player.transform.position.y, player.transform.position.z);
        player.GetComponentInChildren<ParticleSystem>().Clear();
        if (newLevel.GetComponent<LevelData>().id == 5) {
            ResetPlayer();
        }
    }

    private IEnumerator LevelSwitchingCoroutine(float dur, GameObject newLevel, float playerPosX) {

        player.GetComponent<PlayerController>().LockController();
        float time = 0;
        float step = 0.05f;
        while (time < dur) {
            float a = Mathf.Lerp(0, 1, time / dur);
            switchLevelFadeImage.color = new Color(1, 1, 1, a);
            yield return new WaitForSeconds(step);
            time += step;
        }
        SwitchLevels(newLevel, playerPosX);
        time = 0;
        while (time < dur) {
            float a = Mathf.Lerp(1, 0, time / dur);
            switchLevelFadeImage.color = new Color(1, 1, 1, a);
            yield return new WaitForSeconds(step);
            time += step;
        }
        player.GetComponent<PlayerController>().UnlockController();
    }

    public Text LaunchDialogBox(TriggerDialog dialogLauncher) {
        dialogPanel.SetActive(true);
        player.GetComponent<PlayerController>().LockController();
        player.GetComponent<PlayerMode>().LockSwitch();
        return dialogPanel.GetComponentInChildren<Text>();
    }

    public void QuitDialogBox() {
        dialogPanel.SetActive(false);
        player.GetComponent<PlayerController>().UnlockController();
        player.GetComponent<PlayerMode>().UnlockSwitch();
    }

    public void ActivateNextLevelTrigger() {
        nextLevelTrigger.SetActive(true);
    }
}
