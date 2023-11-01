using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string playerTag; // "Player1" 또는 "Player2"를 설정합니다.
    public float moveSpeed = 5f;
    public Animator animator; // Animator 컴포넌트 참조

    private Vector3 ladderStartPosition;
    private Quaternion ladderStartRotation;
    private bool isClimbingLadder = false;
    private float ladderClimbTime = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isClimbingLadder)
        {
            HandleLadderClimb();
            return; // 사다리를 타는 동안에는 다른 움직임을 중지
        }

        Vector3 moveDirection = GetMoveDirection();
        
        HandleAnimation(moveDirection);
        HandleMovement(moveDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder") && !isClimbingLadder)
        {
            ladderStartPosition = transform.position;
            ladderStartRotation = transform.rotation;
            isClimbingLadder = true;
        }
    }

    private Vector3 GetMoveDirection()
    {
        Vector3 moveDirection = Vector3.zero;
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
        ladderClimbTime += Time.deltaTime;

        if (playerTag == "Player1" && Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * 10f * Time.deltaTime;
            animator.SetBool("Climbing", true);
        }
        else if (playerTag == "Player2" && Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * 10f * Time.deltaTime;
            transform.Rotate(Vector3.up, 90f);
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || ladderClimbTime >= 2f)
        {
            ResetLadderState();
        }
    }

private void ResetLadderState()
{
    // 기존 위치와 회전을 사용하지 않고, 새로운 위치로 플레이어를 이동시킵니다.
    Vector3 exitPosition = ladderStartPosition + transform.forward * 1.5f; // 1.5f만큼 앞으로 밀어줍니다. 이 값을 조절하여 원하는 만큼 밀어낼 수 있습니다.
    transform.position = new Vector3(exitPosition.x, ladderStartPosition.y + 10f, exitPosition.z); // 10f는 사다리를 타고 올라간 높이입니다.

    animator.SetBool("Climbing", false);
    isClimbingLadder = false;
    ladderClimbTime = 0f;
}

}
