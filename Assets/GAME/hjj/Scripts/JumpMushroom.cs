using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMushroom : MonoBehaviour
{
    public float jumpForce = 10.0f;
    public bool isPlayer1OnMushroom = false;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            // Debug.Log("player2");
            // 충돌 지점의 normal 벡터를 구함

            ContactPoint contact = collision.contacts[0];
            Vector3 normal = contact.normal.normalized;

            // y 축에서의 충돌 여부 확인
            if (Mathf.Abs(normal.y) > 0.5f) // 이 값을 조정해서 위아래 충돌을 판단합니다.
            { 
                if (isPlayer1OnMushroom)
                {
                    MakePlayer1Jump();
                }
            }
        } else if (collision.gameObject.CompareTag("Player1"))
        {
            // Debug.Log("player1");
            isPlayer1OnMushroom = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            isPlayer1OnMushroom = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            isPlayer1OnMushroom = false;
        }
    }

    void MakePlayer1Jump()
    {
        // Debug.Log("jump");
        // player1 GameObject에 접근하여 Rigidbody 컴포넌트를 이용해 위로 올라가는 동작 구현
        Rigidbody player1Rigidbody = GameObject.FindGameObjectWithTag("Player1").GetComponent<Rigidbody>();
       
        // player1을 위로 올리는 힘을 가해 점프하는 코드
        player1Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

}
