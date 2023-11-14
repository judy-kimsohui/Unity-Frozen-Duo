using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour
{
    public List<Transform> targetPositions; // 객체의 이동할 위치 리스트

    private int currentTargetIndex = 0; // 현재 이동할 위치의 인덱스

    public float moveSpeed = 2.5f; // 이동 속도

    void Update()
    {
        // 리스트가 비어있거나 유효한 위치가 없으면 종료
        if (targetPositions == null || targetPositions.Count == 0 || currentTargetIndex >= targetPositions.Count)
            return;

        // 현재 이동할 위치의 Transform을 가져와 해당 위치로 이동
        Transform currentTarget = targetPositions[currentTargetIndex];
        MoveTowardsTarget(currentTarget);
    }

    void MoveTowardsTarget(Transform target)
    {
        // 현재 위치에서 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        // 만약 현재 위치가 목표 위치에 도달했다면 다음 위치로 인덱스를 이동
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentTargetIndex++;
            // 이동할 위치가 끝에 도달했을 경우 다시 처음 위치로 돌아가기 위해 인덱스를 초기화
            if (currentTargetIndex >= targetPositions.Count)
            {
                currentTargetIndex = 0;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Penguin" || collision.gameObject.name == "Bear")
        {
            GameManager.Instance.GameOver(); // GameManager 스크립트의 GameOver 함수를 호출하는 코드
        }
    }

}
