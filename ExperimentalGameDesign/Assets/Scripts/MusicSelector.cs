using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelector : MonoBehaviour {
    private AudioSource source;
    public AudioClip[] musics;

    private void Awake() {
        source = gameObject.GetComponent<AudioSource>();
        source.clip = musics[Random.Range(0, musics.Length)];
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (!source.isPlaying) {
            source.clip = musics[Random.Range(0, musics.Length)];
            source.Play();
        }
    }
}
