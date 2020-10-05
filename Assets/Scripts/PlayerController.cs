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

    private Animator anim;

    private void Start() {
        footstepSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
            anim.SetBool("isWalking", true);
            if (!footstepSource.isPlaying)
                footstepSource.Play();
        } else {
            anim.SetBool("isWalking", false);
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
