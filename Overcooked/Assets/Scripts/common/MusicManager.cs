using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    public static MusicManager Instance { get; private set; }
    private AudioSource audioSource;
    private float volume;
    private string MUSIC_VOLUME = "musicVolume";

    private void Awake() {
        Instance = this;

        volume = PlayerPrefs.GetFloat(MUSIC_VOLUME, 5f);

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }

    public void ChangeMusicVolume() {
        volume += .1f;
        if (volume > 1f) {
            volume = 0f;
        }

        audioSource.volume = volume;
        PlayerPrefs.SetFloat(MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume() {
        return volume;
    }
}
