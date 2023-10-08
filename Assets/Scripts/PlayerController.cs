using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string playerTag; // "Player1" 또는 "Player2"를 설정합니다.
    public float moveSpeed = 5f;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (playerTag == "Player1")
        {
            // Player1을 움직이는 로직
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
        else if (playerTag == "Player2")
        {
            // Player2를 움직이는 로직
            float horizontalInput = Input.GetAxis("Horizontal2");
            float verticalInput = Input.GetAxis("Vertical2");
            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
