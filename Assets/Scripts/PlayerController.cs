using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private AudioSource footstepSource;
    private Vector3 move;
    private bool lockController = false;

    private void Start() {
        footstepSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (!lockController) {
            move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            transform.position += move * speed * Time.deltaTime;

            if (move != Vector3.zero) {
                if (!footstepSource.isPlaying)
                    footstepSource.Play();
            } else {
                if (footstepSource.isPlaying)
                footstepSource.Stop();
            }
        }
    }

    public void LockController() {
        lockController = true;
    }

    public void UnlockController() {
        lockController = false;
    }

}
