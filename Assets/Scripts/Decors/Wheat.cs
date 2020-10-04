using UnityEngine;

public class Wheat : MonoBehaviour
{


    private SpriteRenderer spriteR;
    public Sprite[] wheatSprites;
    private float flipRandom;


    void Start()
    {

        spriteR = gameObject.GetComponent<SpriteRenderer>();
        spriteR.sprite = wheatSprites[Random.Range(0, wheatSprites.Length)];

        flipRandom = Random.Range(0, 2);
        if(flipRandom >= 1)
        {
            spriteR.flipX = false;
        }
        else
        {
            spriteR.flipX = true;
        }

    }


    void Update()
    {
        


    }
}
