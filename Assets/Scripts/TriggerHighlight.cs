using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHighlight : MonoBehaviour
{

    public GameObject highlightObject;

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.gameObject.tag == "Player") {
            highlightObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {
            highlightObject.SetActive(false);
        }
    }
}
