using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenuManager : MonoBehaviour
{
    public bool isOpen;
    private Rect windowRect;
    private float masterVolume = 100f;
    private float musicVolume = 50f;
    private AudioSource musicPlayerSource;
    private static bool initialized = false;
    void OnEnable() {
        musicPlayerSource = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();
        if (!initialized) {
            initialized = true;
            Debug.Log("Initialized audio for scene.");
            AudioListener.volume = masterVolume / 100f;
            musicPlayerSource.volume = musicVolume / 100f;
        } else {
            Debug.Log("Inherited preserved audio.");
            masterVolume = AudioListener.volume * 100f;
            musicVolume = musicPlayerSource.volume * 100f;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            isOpen = !isOpen;
            if (isOpen) {
                windowRect = new Rect(Screen.width / 2 - 300, Screen.height / 2 - 150, 600, 300);
            }
        }
    }
    void OnGUI() {
        if (isOpen) {
            windowRect = GUILayout.Window(0, windowRect, WindowFn, "Settings");
        }
    }
    void WindowFn(int id) {
        GUILayout.Label("Master Volume:");
        masterVolume = GUILayout.HorizontalSlider(masterVolume, 0, 100);
        AudioListener.volume = masterVolume / 100f;
        GUILayout.Label("");
        GUILayout.Label("Music Volume:");
        musicVolume = GUILayout.HorizontalSlider(musicVolume, 0, 100);
        musicPlayerSource.volume = musicVolume / 100f;

        if (GUILayout.Button("Close")) {
            isOpen = false;
        }
    }
}
