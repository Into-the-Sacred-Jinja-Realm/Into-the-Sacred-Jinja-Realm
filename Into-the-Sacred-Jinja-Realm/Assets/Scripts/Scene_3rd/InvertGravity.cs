using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InvertGravity : MonoBehaviour
{
    public Camera cam;
    public Transform playerTransform;
    public Vector3 gravitydirection = new Vector3(0, -1, 0);
    public float gravityStrength = 9.81f;
    private Rigidbody rb;


    [Header("Rotation Settings")]
    public float rotationDuration = 1.0f; // 旋轉持續時間（秒）
    private bool isRotating = false;
    public float duration = 2.0f;// Duration for the rotation

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
        // cam.transform.rotation = Quaternion.LookRotation(playerTransform.position - cam.transform.position, gravitydirection); // Adjust camera rotation to look at player
        // cam.transform.position = playerTransform.position; // Adjust camera position relative to player
    }


    void LateUpdate()
    {
        cam.transform.position = playerTransform.position;
        Quaternion baseRotation = playerTransform.rotation;
    
        if (isRotating)
            cam.transform.rotation = baseRotation * Quaternion.Euler(0f, 0f, 0f);
        else
            cam.transform.rotation = baseRotation;
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

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0f, 0f, 180f);

        float elapsed = 0f;

        while (elapsed < rotationDuration)
        {
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, elapsed / rotationDuration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;
    }
}
