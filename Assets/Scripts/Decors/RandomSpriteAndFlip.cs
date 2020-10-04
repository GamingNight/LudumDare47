using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteAndFlip : MonoBehaviour
{


    private SpriteRenderer spriteR;
    public Sprite[] allSprites;
    private float flipRandom;


    void Start()
    {

        spriteR = gameObject.GetComponent<SpriteRenderer>();
        spriteR.sprite = allSprites[Random.Range(0, allSprites.Length)];

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


}
