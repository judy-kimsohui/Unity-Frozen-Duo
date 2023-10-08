using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 할당할 변수
    public float smoothSpeed = 0.125f; // 카메라 이동을 부드럽게 하기 위한 스무딩 값

    private Vector3 offset; // 초기 위치와 플레이어 간의 상대적 위치를 저장할 변수

    void Start()
    {
        // 초기 위치와 플레이어 간의 상대적 위치 계산
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // 플레이어의 현재 위치를 가져옵니다.
        Vector3 desiredPosition = player.position + offset;

        // 부드러운 카메라 이동을 위해 Lerp 함수를 사용하여 카메라를 이동시킵니다.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
