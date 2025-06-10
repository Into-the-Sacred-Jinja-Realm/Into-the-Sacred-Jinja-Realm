using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Settings")]
    private Rigidbody rb;
    public Transform cameraTransform;
    private Vector3 cameraForward;
    private Vector3 cameraRight;
    public float rotationSpeed = 5f; // camera rotation speed
    public float jumpForce = 5f;// jump force applied to the player

    [Header("Kinect Settings")]
    private Legs_Listener legs_Listener;
    private Gestures_Listener gestures_Listener;

    [Header("Flags")]
    [SerializeField]private bool canActive = false; // Flag to check if the object can activate the stage

    [Header("Timer")]
    // private float timer = 0f; // Timer to track the time since activation
    public float waitTime = 8f; // Time to wait before allowing activation

    [Header("Speed Control")]
    [SerializeField] private float DefaultSpeed = 10f;
    private float currentSpeed = 0f;
    private float acceleration = 1f;

    Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name != "Scene_1st")
            canActive = true;
            
        rb = GetComponent<Rigidbody>();
        legs_Listener = FindFirstObjectByType<Legs_Listener>();
        gestures_Listener = FindFirstObjectByType<Gestures_Listener>();

        Cursor.lockState = CursorLockMode.Locked;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        currentSpeed = DefaultSpeed;

        if (cameraTransform != null)
        {
            cameraForward = cameraTransform.forward;
            cameraRight = cameraTransform.right;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // extra gravity force
        rb.AddForce(Physics.gravity * 0.5f, ForceMode.Acceleration);


        // Ignore Y axis (move only on the ground)
        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();


        if (Input.GetKey(KeyCode.W))
        {
            Move(true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            Move(false);
        }

        if (cameraTransform != null)
        {
            // Make CameraPosition is behind the player and above the player
            cameraTransform.position = transform.position + new Vector3(0, 2, 0);
        }



    }



    public void Move(bool isMoving)
    {
        Vector3 direction = cameraForward * 1;

        if (isMoving && canActive)
        {
            currentSpeed += acceleration * Time.deltaTime;
            transform.Translate(direction.normalized * currentSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            currentSpeed = DefaultSpeed;
        }
    }

    public void ChangeActive()
    {
        canActive = false;
    }

    public void ChangeActive(float waitTime)
    {
        canActive = false;
        StartCoroutine(WaitAndCanMove(waitTime));
    }

    IEnumerator WaitAndCanMove(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canActive = true;
       
    }

}
