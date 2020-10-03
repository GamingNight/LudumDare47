using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public enum Type
    {
        NEXT, PREVIOUS
    }

    public Type type;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            GameObject currentLevel = GameController.GetInstance().currentLevel;
            GameObject otherLevel = null;
            float newPlayerPosX = 0;
            if (type == Type.NEXT) {
                otherLevel = currentLevel.GetComponent<LevelData>().nextLevel;
                newPlayerPosX = GameController.GetInstance().playerLeftestPosition;
            } else if (type == Type.PREVIOUS) {
                otherLevel = currentLevel.GetComponent<LevelData>().previousLevel;
                newPlayerPosX = GameController.GetInstance().playerRightestPosition;
            }
            if (otherLevel == null)
                return;
            //Switch levels
            otherLevel.SetActive(true);
            currentLevel.SetActive(false);
            GameController.GetInstance().currentLevel = otherLevel;
            //Shift player to the left
            Transform playerTransform = GameController.GetInstance().player.transform;
            playerTransform.position = new Vector3(newPlayerPosX, playerTransform.position.y, playerTransform.position.z);
        }
    }
}
