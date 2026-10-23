using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpForce = 5f;
    private float horizontal;
    private float vertical;
    private Rigidbody rb;

    [Header("Raycast Settings")]
    [SerializeField] private LayerMask groundLayer;     
    [SerializeField] private float rayDistance = 1.1f;
    [SerializeField] private float groundRayOffset = 0.5f;
    [SerializeField] private bool isGrounded;
    [Header("Horizontal Raycast")]
    [SerializeField] private float interactionDistance = 2.5f;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private LayerMask consoleLayer;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        CheckGround();
        CheckHorizontalRaycast();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
       

    }
    private void FixedUpdate()
    {
        Vector3 movement = (transform.forward * vertical + transform.right * horizontal).normalized;
        rb.linearVelocity = new Vector3(movement.x * speed, rb.linearVelocity.y, movement.z * speed);
    }

    private void CheckGround()
    {
        Vector3 origin = transform.position + Vector3.down * groundRayOffset;
        isGrounded = Physics.Raycast(origin, Vector3.down, out RaycastHit hit, rayDistance, groundLayer); 

        //Color ColorRay = condicion ? Variable cuando sea verdadera : variable cuando sea falsa;
        Color ColorRay = isGrounded ? Color.green : Color.red;
        Debug.DrawRay(origin, Vector3.down*rayDistance, ColorRay);
    }
    private void CheckHorizontalRaycast()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
       

        Debug.DrawRay(origin, direction * interactionDistance,Color.blue);
        int combinedLayers = pickupLayer.value | consoleLayer.value;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, interactionDistance, combinedLayers))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ConsoleInteraction console = hit.collider.GetComponentInParent<ConsoleInteraction>();
                if (console != null)
                {
                    console.Activate();
                }
                else
                {
                    Destroy(hit.collider.gameObject);
                    Debug.Log("Object collected!");
                }
            }
        }
    }
    public void RotateCharacter(float rotateY)
    {
        transform.rotation = Quaternion.Euler(0f, rotateY, 0f);
    }
}
