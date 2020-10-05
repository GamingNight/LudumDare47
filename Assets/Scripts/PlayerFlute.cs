using UnityEngine;

[RequireComponent(typeof(AudioFade))]
public class PlayerFlute : MonoBehaviour
{
    public enum Note
    {
        None, Lab, Sib, Do, Reb, Mib
    }

    private AudioFade audioFade;
    private AudioFade secondAudioFade;
    private float pitchOffset;
    private KeyCode lastPressedKey;

    private bool isLocked;

    private CircularBuffer<Note> memory;
    private float memoryDuration;
    private float timeSinceLastNote;

    void Start() {
        audioFade = GetComponent<AudioFade>();
        foreach (Transform child in transform) {
            if (child.GetComponent<AudioFade>() != null)
                secondAudioFade = child.GetComponent<AudioFade>();
        }
        audioFade.source.priority = 0;
        pitchOffset = -1f;
        lastPressedKey = KeyCode.None;
        memory = new CircularBuffer<Note>(10);
        memoryDuration = 2;
        timeSinceLastNote = 0;
    }


    void Update() {

        if (isLocked) {
            memory.Clear();
            return;
        }


        KeyCode pressedKey = KeyCode.None;
        Note notePlayed = Note.None;
        if (Input.GetKeyDown(KeyCode.X)) {
            pitchOffset = 0;
            pressedKey = KeyCode.X;
            notePlayed = Note.Lab;
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            pitchOffset = 2;
            pressedKey = KeyCode.C;
            notePlayed = Note.Sib;
        }
        if (Input.GetKeyDown(KeyCode.V)) {
            pitchOffset = 4;
            pressedKey = KeyCode.V;
            notePlayed = Note.Do;
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            pitchOffset = 5;
            pressedKey = KeyCode.B;
            notePlayed = Note.Reb;
        }
        if (Input.GetKeyDown(KeyCode.N)) {
            pitchOffset = 7;
            pressedKey = KeyCode.N;
            notePlayed = Note.Mib;
        }

        if (pressedKey != KeyCode.None) {
            while (memory.isFull()) {
                memory.Read();
            }
            memory.Add(notePlayed);
            if (!audioFade.isPlaying) {
                if (secondAudioFade.isPlaying)
                    secondAudioFade.StopWithFadeOut();
                audioFade.pitch = Mathf.Pow(2f, (pitchOffset - 4f) / 12.0f);
                audioFade.PlayWithFadeIn();
            } else {
                if (audioFade.isPlaying)
                    audioFade.StopWithFadeOut();
                secondAudioFade.pitch = Mathf.Pow(2f, (pitchOffset - 4f) / 12.0f);
                secondAudioFade.PlayWithFadeIn();
            }
            lastPressedKey = pressedKey;
            timeSinceLastNote = 0;
        } else {
            timeSinceLastNote += Time.deltaTime;
            if (timeSinceLastNote > memoryDuration) {
                memory.ReadAll();
            }
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
        audioFade.StopWithFadeOut();
        secondAudioFade.StopWithFadeOut();
    }

    public void UnlockFlute() {
        isLocked = false;
    }

    public Note[] ReadMemory() {
        return memory.ReadAllNoDelete();
    }
}