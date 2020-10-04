using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RandomSpriteGenerator : MonoBehaviour
{


    public bool randomRotation = false;
    public bool randomFlipX = false;
    public bool randomSprite = false;


    private SpriteRenderer spriteR;
    public Sprite[] allSprites;
    private float flipRandom;


    void Start()
    {

        spriteR = gameObject.GetComponent<SpriteRenderer>();

        if (randomSprite == true)
        {
            spriteR.sprite = allSprites[Random.Range(0, allSprites.Length)];
        }

        if (randomFlipX == true)
        {
            flipRandom = Random.Range(0, 2);
            if (flipRandom >= 1)
            {
                spriteR.flipX = false;
            }
            else
            {
                spriteR.flipX = true;
            }
        }

        if (randomRotation == true)
        {
            transform.eulerAngles = new Vector3(
                transform.rotation.x,
                transform.rotation.y,
                transform.rotation.z + Random.Range(0f, 360f)
            );
        }


    }
}
