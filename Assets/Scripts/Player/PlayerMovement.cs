using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public Transform playerBottom;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float gravity = -22.81f;
    public float jumpHeight = 3f;
    Vector3 velocity;
    bool isGrounded;

    public PostProcessVolume PostProcess;

    private ChromaticAberration aberration;
    void Start()
    {
        PostProcess.profile.TryGetSettings(out aberration);
    }

    void Update()
    {
      if (isGrounded && velocity.y < 0) {
        velocity.y = -2f;
      }
      if (Input.GetKey(KeyCode.LeftShift)) {
        speed = 6f;
      } else speed = 4f;
      isGrounded = Physics.CheckSphere(playerBottom.position, groundDistance, groundMask);
      float x = Input.GetAxis("Horizontal");
      float z = Input.GetAxis("Vertical");
      Vector3 move = transform.right * x + transform.forward * z;
      controller.Move(move * speed * Time.deltaTime);

     if (isGrounded && Input.GetButtonDown("Jump")) {
       velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
     }

     if (Input.GetKeyDown(KeyCode.LeftShift)) {
       speed = 6f;
       aberration.intensity.value = 0.3f;
     } else if (!Input.GetKey(KeyCode.LeftShift)) {
       speed = 4f;
       aberration.intensity.value = 0f;
     }

      velocity.y += gravity * Time.deltaTime;
      controller.Move(velocity * Time.deltaTime);
    }
}
