using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoserScript: MonoBehaviour
{
    public AudioClip loser;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.Play(loser);
    }
    
}
