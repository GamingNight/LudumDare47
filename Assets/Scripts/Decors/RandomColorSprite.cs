using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorSprite : MonoBehaviour
{


    private SpriteRenderer spriteR;

    public bool autumnSeason = false;

    private Color selectedColor;
    public Color[] summerColorsList;
    public Color[] autumnColorsList;


    void Start()
    {

        if (autumnSeason == true)
        {

            selectedColor = autumnColorsList[UnityEngine.Random.Range(0, autumnColorsList.Length)];

        }
        else
        {

            selectedColor = summerColorsList[UnityEngine.Random.Range(0, summerColorsList.Length)];

        }

        spriteR = gameObject.GetComponent<SpriteRenderer>();
        spriteR.color = selectedColor;


    }

}
