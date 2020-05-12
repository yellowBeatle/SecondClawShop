using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    public Camera myCamera;
    private Vector3 direction = Vector3.zero;
    public float speed = 6f;
    //public float gravity = 10f;
    //public float jumpForce = 8f;
    float cameraAngleX;
    public float mouseSensibility = 8f;
    public bool invertY = true;

    //JUmp
    public float _MaxJumpHeight;
    public float _MinJumpHeight;
    public float _TimeToJumpMaxHeight;
    float _Gravity;
    float _MaxJumpVelocity;
    float _MinJumpVelocity;

    [Tooltip("Limits the angle the camera can rotate")]public float clampY = 45f;

    private bool enableRotation = true;

    void Start()
    {
        cameraAngleX = 0;
        Cursor.lockState = CursorLockMode.Locked;
        _Gravity = (2 * _MaxJumpHeight) / Mathf.Pow(_TimeToJumpMaxHeight, 2);
        _MaxJumpVelocity = Mathf.Abs(_Gravity) * _TimeToJumpMaxHeight;
        _MinJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(_Gravity) + _MaxJumpHeight);
    }

    void Update ()
    {
        //LOCK THE CURSOR IN THE CENTER OF THE SCREEN
        if(Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            EnableRotation();
        }

        //UNLOCK THE CURSOR TO EXIT THE GAME
        if(Input.GetAxis("Cancel") > 0)
        {
            Cursor.lockState = CursorLockMode.None;
            DisableRotation();
        }


        Movement();

        if(enableRotation)
        {
            CameraRotation();
        }
	}

    void Movement()
    {
        //JUMP
        if(characterController.isGrounded)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                direction.y = _MaxJumpVelocity;
            }
            
        }
        else
        {

            direction.y -= _Gravity * Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (direction.y > _MinJumpVelocity)
                {
                    direction.y = _MinJumpVelocity;

                }
            }
        }

        if (characterController.collisionFlags == CollisionFlags.Above && direction.y > 0)
        {
            direction.y = 0;
        }

        //WASD INPUT
        Vector2 normalizedInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (normalizedInput.magnitude > 1)
            normalizedInput = normalizedInput.normalized;
        float dirX = normalizedInput.x;
        float dirY = normalizedInput.y;

        //ADD A SPEED MULTIPLAYER IF THE PLAYER HOLDS SHIFT
        float runMultiplier = Input.GetAxisRaw("Run") > 0 ? 1.5f : 1f;

        direction.x = dirX * speed * runMultiplier;
        direction.z = dirY * speed * runMultiplier;

        direction = transform.TransformDirection(direction);

        characterController.Move(direction * Time.deltaTime);
    }

    void CameraRotation()
    {
        //MOUSE INPUT
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //INVERT DE Y CAMERA MOVEMENT IF invertY IS TRUE
        int invert = invertY ? -1 : 1;

        cameraAngleX += mouseY * mouseSensibility * invert;
        cameraAngleX = Mathf.Clamp(cameraAngleX, -clampY, clampY);

        //THE FUNCTION CONVERTS THE FLOAT DISPLACEMENTS IN TO QUATERNIONS
        Quaternion angleX = Quaternion.Euler(0, mouseX * mouseSensibility, 0);
        Quaternion angleY = Quaternion.Euler(cameraAngleX, 0, 0);

        //FOR THE X AXIS ROTATE THE PLAYER, FOR THE Y AXIS ROTATE THE CAMERA
        transform.localRotation *= angleX;
        myCamera.transform.localRotation = angleY;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //SIMULATE A FORCE IF THE PLAYER COLLIDE WITH A RIGIDBODY
        if(characterController.collisionFlags == CollisionFlags.Below)
            return;

        Rigidbody body = hit.collider.attachedRigidbody;

        if(body == null || body.isKinematic)
            return;

        body.AddForceAtPosition(characterController.velocity * .3f, hit.point, ForceMode.Impulse);
    }

    public void EnableRotation()
    {
        enableRotation = true;
    }

    public void DisableRotation()
    {
        enableRotation = false;
    }
}