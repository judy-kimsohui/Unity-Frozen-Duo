using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallbreaking : MonoBehaviour
{
    // 캐릭터가 벽에 닿았는지 확인하는 플래그입니다.
    private bool polarBearTouching = false;
    private bool penguinTouching = false;

    // 벽에 Rigidbody를 추가하는 함수입니다.
    private void TryAddRigidbodyToChildren()
    {
        // 북극곰과 펭귄이 모두 벽을 밀고 있을 경우에만 작동
        if (polarBearTouching && penguinTouching)
        {
            foreach (Transform child in transform)
            {
                // 자식 오브젝트에 이미 Rigidbody가 없는 경우에만 추가
                if (child.GetComponent<Rigidbody>() == null)
                {
                    Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
                    // Rigidbody 설정을 원하는 대로 조정할 수 있습니다.
                    rb.mass = 1; // 예를 들어, 질량을 설정합니다.
                    // 추가적인 Rigidbody 설정을 여기에 구현할 수 있습니다.

                    Destroy(gameObject, 5f);
                }
            }
        }
    }

    // OnCollisionEnter는 오브젝트가 충돌했을 때 호출됩니다.
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트의 태그를 확인하여 북극곰과 펭귄이 벽에 닿았는지 확인합니다.
        if (collision.gameObject.tag == "Player1")
        {
            polarBearTouching = true;
        }
        else if (collision.gameObject.tag == "Player2")
        {
            penguinTouching = true;
        }

        // Rigidbody를 추가하는 시도를 합니다.
        TryAddRigidbodyToChildren();

    }

    // OnCollisionExit는 오브젝트가 충돌에서 벗어났을 때 호출됩니다.
    private void OnCollisionExit(Collision collision)
    {
        // 충돌한 오브젝트가 벗어나면 플래그를 다시 false로 설정합니다.
        if (collision.gameObject.tag == "Player1")
        {
            polarBearTouching = false;
        }
        else if (collision.gameObject.tag == "Player2")
        {
            penguinTouching = false;
        }
    }
}
