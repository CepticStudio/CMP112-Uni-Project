using Unity.Hierarchy;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    [SerializeField] Transform playerCamera;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool cursorlock = true;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float speed = 2.0f;
    [SerializeField][Range(0.0f, 0.5f)] float  moveSmoothTime = 0.3f;
    [SerializeField] float gravity = -30f;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask ground;


    public float jumpheight = 1.0f;
    float velocityY;
    bool isGrounded;

    float CameraCap;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    CharacterController controller;
    Vector2 currentDir;
    Vector2 currentDirVeloctiy;
    Vector3 velocity;

     void Start()
    {
     controller = GetComponent<CharacterController>();
        
        if (cursorlock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;

        }
           
    }

    void Update()
    {
        UpdateMove();
        UpdateMouse();
    }



    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        CameraCap -= currentMouseDelta.y * mouseSensitivity;

        CameraCap = Mathf.Clamp(CameraCap, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * CameraCap;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);

    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, 0.2f, ground);

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVeloctiy, mouseSmoothTime);

        velocityY += gravity * 2f * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * speed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocityY = Mathf.Sqrt(jumpheight * -2f * gravity);
        }

        if(isGrounded! && controller.velocity.y < -1f)
        {
            velocityY = -8f;
        }

    }

}