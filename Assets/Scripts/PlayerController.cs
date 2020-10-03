using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Vector3 move;
    private bool lockController = false;

    void Update() {
        if (!lockController) {
            move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.position += move * speed * Time.deltaTime;
        }
    }

    public void LockController() {
        lockController = true;
    }

    public void UnlockController() {
        lockController = false;
    }

}
