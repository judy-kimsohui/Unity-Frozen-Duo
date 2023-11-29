using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EndingTrigger : MonoBehaviour
{
    public PlayableDirector director;

    void Start()
    { 
        director = GetComponent<PlayableDirector>();
        if (director == null)
        {
            Debug.LogError("PlayableDirector 컴포넌트를 찾을 수 없습니다.");
        }
    }

    void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            director.Play();
	    }
    }
}
