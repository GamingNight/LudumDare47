using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autel : MonoBehaviour
{

    private bool isHighlighted;
    private Animator anim;

    private void Start()
    {
        isHighlighted = false;
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            isHighlighted = true;
            anim.SetBool("playerClose", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            isHighlighted = false;
            anim.SetBool("playerClose", false);
        }
    }

    public bool IsHighLighted()
    {
        return isHighlighted;
    }
}
