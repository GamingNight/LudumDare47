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
            GameController.GetInstance().LaunchLevelSwitching(type);
        }
    }
}
