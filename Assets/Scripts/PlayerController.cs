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
        float leftCameraLimit = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, -Camera.main.transform.position.z)).x;
        transform.position = new Vector3(Mathf.Max(leftCameraLimit, transform.position.x), transform.position.y, transform.position.z);

        if (horizontal < 0) {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        } else if (horizontal > 0) {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
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
        footstepSource.Stop();
        anim.SetBool("isWalking", false);
    }

    public void UnlockController() {
        lockController = false;
    }

}
