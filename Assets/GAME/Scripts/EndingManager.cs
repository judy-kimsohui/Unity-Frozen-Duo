using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EndingManager : MonoBehaviour
{
    public PlayableDirector director; // Inspector에서 Playable Director를 할당하세요.

    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            director.Play();
	    }
    }

}
