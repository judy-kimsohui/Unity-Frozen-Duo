using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string playerTag; // "Player1" 또는 "Player2"를 설정합니다.
    public float moveSpeed = 5f;
    public Camera playerCamera; // 플레이어 카메라 참조
    public Animator animator; // Animator 컴포넌트 참조
    public Rigidbody rb;

    private Vector3 ladderStartPosition;
    private float climbSpeed = 3f;
    private float ladderYSize;
    private Quaternion ladderStartRotation;
    private bool isClimbingLadder = false;

    private float cameraYAngle = 0f; // 카메라의 Y축 회전 각도

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cameraYAngle = playerCamera.transform.eulerAngles.y;

        // 카메라의 초기 위치 설정
        SetInitialCameraPosition();
    }

    private void SetInitialCameraPosition()
    {
        // 카메라를 플레이어 뒤에 배치하고, 플레이어를 바라보게 설정
        playerCamera.transform.position = transform.position + new Vector3(0, 2, -5); // 예시 위치, 필요에 따라 조정
        playerCamera.transform.LookAt(transform.position);
    }

    private void Update()
    {
        if (isClimbingLadder)
        { 
            HandleLadderClimb();
            HandleCameraRotation();
            return; // 사다리를 타는 동안에는 다른 움직임을 중지
        }


        HandleCameraRotation();
        Vector3 moveDirection = GetMoveDirection();
        
        HandleAnimation(moveDirection);
        HandleMovement(moveDirection);
    }
    private void HandleCameraRotation()
    {
        float rotationSpeed = 5f; // 원하는 회전 속도 설정

        // 카메라 회전 각도 업데이트
        if (playerTag == "Player1")
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                cameraYAngle += rotationSpeed; // 시계 방향 회전
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                cameraYAngle -= rotationSpeed; // 반시계 방향 회전
            }
        }
        else if (playerTag == "Player2")
        {
            if (Input.GetKey(KeyCode.RightShift))
            {
                cameraYAngle += rotationSpeed; // 시계 방향 회전
            }
            else if (Input.GetKey(KeyCode.Slash))
            {
                cameraYAngle -= rotationSpeed; // 반시계 방향 회전
            }
        }

        // 카메라의 위치와 회전을 플레이어 위치에 상대적으로 설정
        playerCamera.transform.position = transform.position + Quaternion.Euler(0, cameraYAngle, 0) * new Vector3(0, 2, -5); // 카메라 위치 조정
        playerCamera.transform.LookAt(transform.position); // 카메라가 플레이어를 바라보도록 설정
    }


    private Vector3 GetMoveDirection()
    {
        Vector3 moveDirection = Vector3.zero;
        // 플레이어의 입력에 따른 기본 방향 설정
        if (playerTag == "Player1")
        {
            if (Input.GetKey(KeyCode.W)) moveDirection += Vector3.forward;
            if (Input.GetKey(KeyCode.S)) moveDirection += Vector3.back;
            if (Input.GetKey(KeyCode.A)) moveDirection += Vector3.left;
            if (Input.GetKey(KeyCode.D)) moveDirection += Vector3.right;
        }
        else if (playerTag == "Player2")
        {
            if (Input.GetKey(KeyCode.UpArrow)) moveDirection += Vector3.forward;
            if (Input.GetKey(KeyCode.DownArrow)) moveDirection += Vector3.back;
            if (Input.GetKey(KeyCode.LeftArrow)) moveDirection += Vector3.left;
            if (Input.GetKey(KeyCode.RightArrow)) moveDirection += Vector3.right;
        }

        // 카메라 Y축 회전에 따른 이동 방향 조정
        Quaternion cameraRotation = Quaternion.Euler(0, cameraYAngle, 0);
        moveDirection = cameraRotation * moveDirection;
        return moveDirection;
    }

    private void HandleAnimation(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("Walking", true);
            Quaternion lookRotation = Quaternion.LookRotation(moveDirection.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void HandleMovement(Vector3 moveDirection)
    {
        transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
    }

    private void HandleLadderClimb()
    {

        if (playerTag == "Player1")
        {
            animator.SetBool("Climbing", true);
            if (Input.GetKey(KeyCode.W))
                transform.position += Vector3.up * climbSpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.S))
			    transform.position += Vector3.down * climbSpeed * Time.deltaTime;
        }
        else if (playerTag == "Player2")
        {
            //transform.Rotate(Vector3.up, 90f);
            if (Input.GetKey(KeyCode.UpArrow))
                transform.position += Vector3.up * climbSpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.DownArrow))
                transform.position += Vector3.down * climbSpeed * Time.deltaTime;
        }
    }

    void  OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            rb.useGravity = true;
            Vector3 exitPosition = ladderStartPosition + transform.forward * 0.5f;
            transform.position = new Vector3(exitPosition.x, ladderYSize / 2 + 2.0f, exitPosition.z);

            animator.SetBool("Climbing", false);
            isClimbingLadder = false;
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            //사다리에 플레이어를 고정 시키기 위해 중력끄고 속도 0으로 만듬
            rb.useGravity = false;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            //사다리의 y크기를 받아와서 사다리를 나올 때 사용
            ladderYSize = other.bounds.size.y;
            ladderStartPosition = other.transform.position;
            isClimbingLadder = true;

            transform.rotation = other.transform.rotation;
        }

        if(other.gameObject.CompareTag("Trap"))
        {
            GameManager.Instance.GameOver();
        }
    }

}
