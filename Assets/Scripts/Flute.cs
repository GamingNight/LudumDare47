using UnityEngine;

[RequireComponent(typeof(AudioFade))]
public class Flute : MonoBehaviour
{
    private AudioFade audioFade;
    private float note;

    void Start() {
        audioFade = GetComponent<AudioFade>();
        audioFade.source.priority = 0;
        note = -1f;
    }


    void Update() {

        if (Input.GetKeyDown("x")) note = 0;
        if (Input.GetKeyDown("c")) note = 2;
        if (Input.GetKeyDown("v")) note = 4;
        if (Input.GetKeyDown("b")) note = 5;
        if (Input.GetKeyDown("n")) note = 7;

        if (Input.GetKeyDown("x") || Input.GetKeyDown("c") || Input.GetKeyDown("v") || Input.GetKeyDown("b") || Input.GetKeyDown("n")) {
            audioFade.pitch = Mathf.Pow(2f, (note - 4f) / 12.0f);
            //audioFade.PlayWithFadeIn();
            audioFade.Play();
        } else if (Input.GetKeyUp("x") || Input.GetKeyUp("c") || Input.GetKeyUp("v") || Input.GetKeyUp("b") || Input.GetKeyUp("n")) {
            audioFade.StopWithFadeOut();
        }
    }
}