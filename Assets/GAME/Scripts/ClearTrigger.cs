using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1")|| other.CompareTag("Player2"))
        {
            Debug.Log("Clear Triggered");
            GameManager.Instance.GameClear(); // GameManager의 GameClear() 함수 호출
        }
    }
}
