using UnityEngine;

[RequireComponent(typeof(AudioFade))]
public class Flute : MonoBehaviour
{
    private AudioFade audioFade;
    private AudioFade secondAudioFade;
    private float note;
    private KeyCode lastPressedKey;

    private bool isLocked;

    void Start() {
        audioFade = GetComponent<AudioFade>();
        foreach (Transform child in transform) {
            if (child.GetComponent<AudioFade>() != null)
                secondAudioFade = child.GetComponent<AudioFade>();
        }
        audioFade.source.priority = 0;
        note = -1f;
        lastPressedKey = KeyCode.None;
    }


    void Update() {

        if (isLocked)
            return;

        KeyCode pressedKey = KeyCode.None;
        if (Input.GetKeyDown(KeyCode.X)) {
            note = 0;
            pressedKey = KeyCode.X;
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            note = 2;
            pressedKey = KeyCode.C;
        }
        if (Input.GetKeyDown(KeyCode.V)) {
            note = 4;
            pressedKey = KeyCode.V;
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            note = 5;
            pressedKey = KeyCode.B;
        }
        if (Input.GetKeyDown(KeyCode.N)) {
            note = 7;
            pressedKey = KeyCode.N;
        }

        if (pressedKey != KeyCode.None) {
            if (!audioFade.isPlaying) {
                if (secondAudioFade.isPlaying)
                    secondAudioFade.StopWithFadeOut();
                audioFade.pitch = Mathf.Pow(2f, (note - 4f) / 12.0f);
                audioFade.PlayWithFadeIn();
            } else {
                if (audioFade.isPlaying)
                    audioFade.StopWithFadeOut();
                secondAudioFade.pitch = Mathf.Pow(2f, (note - 4f) / 12.0f);
                secondAudioFade.PlayWithFadeIn();
            }
            lastPressedKey = pressedKey;
        }
        if (Input.GetKeyUp(lastPressedKey)) {
            if (audioFade.isPlaying)
                audioFade.StopWithFadeOut();
            if (secondAudioFade.isPlaying)
                secondAudioFade.StopWithFadeOut();
        }
    }

    public void LockFlute() {
        isLocked = true;
    }

    public void UnlockFlute() {
        isLocked = false;
    }
}