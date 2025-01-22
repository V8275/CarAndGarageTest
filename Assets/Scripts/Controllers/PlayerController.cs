using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform cameraTransform;
    public float lookSpeed = 2f; // Скорость поворота
    public float sensitivity = 1f; // Чувствительность

    private Rigidbody rb;
    private bool isGrounded;
    private IInput inputService;

    [Inject]
    public void Construct(IInput inputService)
    {
        this.inputService = inputService;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
        LookAround();
        inputService.HandleInteraction(cameraTransform); // Обработка взаимодействия
    }

    void Move()
    {
        Vector2 input = inputService.GetMovementInput();
        Vector3 direction = new Vector3(input.x, 0, input.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDir = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0) * direction;
            rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        if (isGrounded && inputService.IsJumpPressed())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void LookAround()
    {
        Vector2 lookInput = inputService.GetLookInput();
        transform.Rotate(0, lookInput.x * lookSpeed, 0);

        float newRotationX = cameraTransform.localEulerAngles.x - lookInput.y * lookSpeed;

        if (newRotationX > 180)
        {
            newRotationX -= 360;
        }

        if (newRotationX < -80f)
        {
            newRotationX = -80f;
        }
        else if (newRotationX > 80f)
        {
            newRotationX = 80f;
        }

        cameraTransform.localEulerAngles = new Vector3(newRotationX, 0, 0);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
