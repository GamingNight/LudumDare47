using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private AudioSource footstepSource;
    private SpriteRenderer spriteRenderer;
    private Vector3 move;
    private bool lockController = false;

    private void Start() {
        footstepSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {

        if (lockController)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        move = new Vector3(horizontal, 0, 0);
        transform.position += move * speed * Time.deltaTime;

        if (horizontal < 0) {
            spriteRenderer.flipX = true;
        } else if (horizontal > 0) {
            spriteRenderer.flipX = false;
        }

        if (move != Vector3.zero) {
            if (!footstepSource.isPlaying)
                footstepSource.Play();
        } else {
            if (footstepSource.isPlaying)
                footstepSource.Stop();
        }
    }

    public void LockController() {
        lockController = true;
    }

    public void UnlockController() {
        lockController = false;
    }

}
