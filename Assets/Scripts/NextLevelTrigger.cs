using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            GameObject currentLevel = GameController.GetInstance().currentLevel;
            GameObject nextLevel = currentLevel.GetComponent<LevelData>().nextLevel;
            //Switch levels
            nextLevel.SetActive(true);
            currentLevel.SetActive(false);
            //Shift player to the left
            Transform playerTransform = GameController.GetInstance().player.transform;
            playerTransform.position = new Vector3(GameController.GetInstance().playerLeftestPosition, playerTransform.position.y, playerTransform.position.z);
        }
    }
}
