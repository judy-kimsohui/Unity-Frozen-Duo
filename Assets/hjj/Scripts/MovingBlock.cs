using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    // Penguin과 Bear를 식별하기 위한 이름
    public string penguinName = "Penguin";
    public string bearName = "PolarBear";

    // 이동 속도
    public float moveDistance = 2.0f; // 이동 거리를 조정해보세요.

    private bool isMoving = false;

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 Penguin인지 확인
        if (collision.gameObject.name == penguinName)
        {
            // Penguin인 경우 움직이지 않음
            // Debug.Log("Penguin은 움직일 수 없습니다!");
        }
        else if (collision.gameObject.name == bearName && !isMoving)
        {
            // 충돌 지점의 normal 벡터를 구함
            ContactPoint contact = collision.contacts[0];
            Vector3 normal = contact.normal.normalized;

            // y 축에서의 충돌 여부 확인
            if (Mathf.Abs(normal.y) < 0.5f) // 이 값을 조정해서 위아래 충돌을 판단합니다.
            {
                // x, z 축의 비중을 계산하여 더 비중이 큰 축으로 이동 방향 설정
                Vector3 direction = Vector3.zero;
                if (Mathf.Abs(normal.x) > Mathf.Abs(normal.z))
                {
                    direction = new Vector3(Mathf.Sign(normal.x), 0, 0);
                }
                else
                {
                    direction = new Vector3(0, 0, Mathf.Sign(normal.z));
                }

                // 이동 속도와 방향 벡터의 크기를 곱하여 이동 벡터를 얻음
                Vector3 moveVector = direction * moveDistance;

                // 오브젝트를 부드럽게 이동시킴
                StartCoroutine(MoveBlockSmoothly(transform.position, transform.position + moveVector, 1.0f));
            }
        }
    }

    // 보간을 통해 부드럽게 이동
    private System.Collections.IEnumerator MoveBlockSmoothly(Vector3 start, Vector3 end, float timeToMove)
    {
        isMoving = true;

        float elapsedTime = 0.0f;
        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(start, end, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isMoving = false;
    }
}
