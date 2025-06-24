using UnityEngine;

public class InvertGravityCollider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private CameraMovement CameraMovement;
    private InvertGravity_Ver2 invertGravity_Ver2;
    void Start()
    {
        CameraMovement = FindFirstObjectByType<CameraMovement>();
        invertGravity_Ver2 =FindFirstObjectByType<InvertGravity_Ver2>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraMovement.ChangeActive(3f);
            invertGravity_Ver2.invert();
        }
    }
}
