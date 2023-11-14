using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    // Penguin과 Bear를 식별하기 위한 이름
    public string penguinName = "Penguin";
    public string bearName = "PolarBear";

    // 이동 속도
    public float moveSpeed = 5.0f;


    private Vector3 velocity = Vector3.zero;

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 Penguin인지 확인
        if (collision.gameObject.name == penguinName)
        {
            // Penguin인 경우 움직이지 않음
            Debug.Log("Penguin은 움직일 수 없습니다!");
        }
        else if (collision.gameObject.name == bearName)
        {
            Debug.Log("Bear");

            // 충돌 지점의 normal 벡터를 구함
            ContactPoint contact = collision.contacts[0];
            Vector3 normal = contact.normal.normalized;

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

            // 블록을 충돌한 방향으로 보간하여 부드럽게 이동
            StartCoroutine(MoveBlockSmoothly(direction));
        }
    }

    // 보간을 통해 부드럽게 이동
    private System.Collections.IEnumerator MoveBlockSmoothly(Vector3 direction)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = transform.position + direction;

        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float startTime = Time.time;

        while (transform.position != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            yield return null;
        }
    }
}
