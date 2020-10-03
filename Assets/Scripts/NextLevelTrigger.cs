using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            GameObject nextLevel = transform.parent.GetComponent<LevelData>().nextLevel;
            nextLevel.SetActive(true);
            transform.parent.gameObject.SetActive(false);
            Transform playerTransform = GameController.GetInstance().player.transform;
            playerTransform.position = new Vector3(GameController.GetInstance().playerLeftestPosition, playerTransform.position.y, playerTransform.position.z);
        }
    }
}
