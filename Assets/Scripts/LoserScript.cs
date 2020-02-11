using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loser : MonoBehaviour
{
    public AudioClip sound;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.Play(sound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
