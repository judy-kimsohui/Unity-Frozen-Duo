using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IceBreaking : MonoBehaviour
{
    private int touchCount = 0; // 벽에 닿은 횟수를 카운트합니다.

    // 자식 오브젝트들에게 Rigidbody를 추가하는 함수
    private void AddRigidbodyToChildren()
    {
        foreach (Transform child in transform)
        {
            // 자식 오브젝트에 Rigidbody가 없으면 추가합니다.
            if (child.GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
                // Rigidbody의 다양한 설정을 여기에서 할 수 있습니다.
                rb.mass = 1; // 예를 들어, 질량을 설정합니다.
                // 추가적인 Rigidbody 설정을 여기에 구현할 수 있습니다.
            }
        }
    }

    // 오브젝트가 벽과 충돌했을 때 호출됩니다.
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트의 태그가 "Player1" 또는 "Player2"인지 확인합니다.
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            touchCount++; // 닿은 횟수를 증가시킵니다.
            
            // 3번 이상 닿았을 경우에 자식 오브젝트들에 Rigidbody를 추가합니다.
            if (touchCount >= 5)
            {
                AddRigidbodyToChildren();
            }
        }
    }
}
