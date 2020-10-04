using UnityEngine;
using System.Collections;
using System.Diagnostics;

[RequireComponent(typeof(AudioSource))]
public class Flute : MonoBehaviour
{

    public AudioClip C4;

    public float note;


    void Start()
    {

        AudioSource audio = GetComponent<AudioSource>();
        note = -1f;

    }


    void Update()
    {

        if (Input.GetKeyDown("x")) note = 0;
        if (Input.GetKeyDown("c")) note = 2;
        if (Input.GetKeyDown("v")) note = 4;
        if (Input.GetKeyDown("b")) note = 5;
        if (Input.GetKeyDown("n")) note = 7;

        if (Input.GetKeyDown("x") || Input.GetKeyDown("c") || Input.GetKeyDown("v") || Input.GetKeyDown("b") || Input.GetKeyDown("n"))
        {
            GetComponent<AudioSource>().pitch = Mathf.Pow(2f, (note-4f)/ 12.0f);
            GetComponent<AudioSource>().Play();
        }

    }


}
