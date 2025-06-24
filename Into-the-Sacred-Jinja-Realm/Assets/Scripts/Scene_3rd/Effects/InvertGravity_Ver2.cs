using System.Collections;
using UnityEngine;

public class InvertGravity_Ver2 : MonoBehaviour
{
   public Camera cam;
    public Transform playerTransform;
    public Vector3 gravitydirection = new Vector3(0, -1, 0);
    public float gravityStrength = 9.81f;
    private Rigidbody rb;
    [SerializeField]private Scene3CamRotate Scene3CamRotate;


    [Header("Rotation Settings")]
    public float rotationDuration = 1.0f; // Duration for the rotation
    private bool isRotating = false;
    [SerializeField]private float Rduration = 2.0f;
     

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Disable the default gravity
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& !isRotating) // Check if the space key is pressed and not currently rotating  
        {
            invert(); // Call the invert method when the space key is pressed
        }
        
    }


    void LateUpdate()
    {
        if (Scene3CamRotate.IsScene3)
        {
            cam.transform.position = playerTransform.position;
            Quaternion baseRotation = playerTransform.rotation;

            if (isRotating)
                cam.transform.rotation = baseRotation * Quaternion.Euler(0f, 0f, 0f);
            else
                cam.transform.rotation = baseRotation;
        }
        //cam.transform.rotation = Quaternion.LookRotation(playerTransform.position - cam.transform.position, gravitydirection); // Adjust camera rotation to look at player
    }

    void FixedUpdate()
    {
        rb.AddForce(gravitydirection * gravityStrength, ForceMode.Acceleration);
    }
    public void invert()
    {
        gravitydirection = -gravitydirection; // Invert the gravity direction
        if (!isRotating)
        {
            StartCoroutine(RotateOverTime());
        }
    }


    private IEnumerator RotateOverTime()
    {
        isRotating = true;

        Quaternion StartRotation = transform.rotation;
        // Vector3 StartPosition = transform.position;
        float TargetRotation = StartRotation.eulerAngles.z + 180f;
        float CurrentRotation = transform.rotation.eulerAngles.z;
        // float TargetPosition = StartPosition.x + 20f;
        // float CurrentPosition = transform.position.x;

        while (Mathf.Abs(Mathf.DeltaAngle(CurrentRotation, TargetRotation)) > 0.1f)
        {
            CurrentRotation = Mathf.SmoothDamp(CurrentRotation, TargetRotation, ref Rduration, rotationDuration);
            transform.rotation = Quaternion.Euler(StartRotation.eulerAngles.x, StartRotation.eulerAngles.y, CurrentRotation + 0.1f);
            // CurrentPosition = Mathf.SmoothDamp(CurrentPosition, TargetPosition, ref Pduration, rotationDuration);
            // transform.position = new Vector3(CurrentPosition, StartPosition.y, StartPosition.z);
            yield return null;
        }// Ensure the rotation is exactly 180 degrees
        isRotating = false;
    }
}
