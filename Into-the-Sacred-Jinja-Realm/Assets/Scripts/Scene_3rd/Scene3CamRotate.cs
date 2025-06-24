using System.Collections;
using UnityEngine;

public class Scene3CamRotate : MonoBehaviour
{
    public float duration = 0f;
    private float TargetRotation; // Target rotation angle
    public bool IsScene3 = false;
    public bool Rotating = true; // Flag to indicate if this is Scene 3
    private Quaternion StartRotation;
    public Material[] Cloud;
    public Material Hands;
    public GameObject[] PlayerHands;// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartRotation = transform.rotation;
        TargetRotation = StartRotation.eulerAngles.x - 45f; // Set the flag to true when the script starts
    }
    void Update()
    {
        if (Rotating)
        {
            StartCoroutine(CamRotate());
            Rotating = false; // Set the flag to false to prevent multiple rotations
        }
        float CurrentRotation = transform.rotation.eulerAngles.x;
        if (Mathf.Abs(Mathf.DeltaAngle(CurrentRotation, TargetRotation)) < 0.1f && !IsScene3)
        {
            IsScene3 = true; // Reset the flag if rotation is not complete
        }
    }

    // Update is called once per frame
    public IEnumerator CamRotate()
    {
        TargetRotation = StartRotation.eulerAngles.x - 45f;
        float CurrentRotation = transform.rotation.eulerAngles.x;
        while (Mathf.Abs(Mathf.DeltaAngle(CurrentRotation, TargetRotation)) > 0.1f)
        {
            CurrentRotation = Mathf.SmoothDampAngle(CurrentRotation, TargetRotation, ref duration, 0.5f);
            transform.rotation = Quaternion.Euler(CurrentRotation - 0.1f, StartRotation.eulerAngles.y, StartRotation.eulerAngles.z);
            yield return null;
        }
        if (Mathf.Abs(Mathf.DeltaAngle(CurrentRotation, TargetRotation)) > 0.1f)
        { // Reset the flag if rotation is not complete
            yield break;
        }
    }
}
