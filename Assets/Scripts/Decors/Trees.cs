using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{

    private Animator anim;
    private float randomStartAnim;

    private SpriteRenderer spriteR;
    public bool autumnTree = false;

    private Color selectedColor;
    public Color[] summerColorsList;
    public Color[] autumnColorsList;


    void Start()
    {


        randomStartAnim = UnityEngine.Random.Range(0.8f, 1.2f);

        anim = GetComponent<Animator>();
        anim.SetFloat("randomstart", randomStartAnim);
        anim.speed = randomStartAnim;


        if (autumnTree == true)
        {

            selectedColor = autumnColorsList[UnityEngine.Random.Range(0, autumnColorsList.Length)];

        } else
        {

            selectedColor = summerColorsList[UnityEngine.Random.Range(0, summerColorsList.Length)];

        }

        spriteR = gameObject.GetComponent<SpriteRenderer>();
        spriteR.color = selectedColor;



    }

}
