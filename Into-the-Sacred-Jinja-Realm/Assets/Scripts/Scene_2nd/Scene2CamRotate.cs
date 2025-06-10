using System;
using System.Collections;
using UnityEngine;

public class Scene2CamRotate : MonoBehaviour
{
    private AnimationController AnimationController;
    public float duration = 0f;
    private Quaternion StartRotation;// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AnimationController = FindFirstObjectByType<AnimationController>();
        StartRotation = transform.rotation;
    }

    // Update is called once per frame
    public IEnumerator CamRotate(bool IsStart)
    {
        if (IsStart)
        {
            float TargetRotation = StartRotation.eulerAngles.x + 45f;
            float CurrentRotation = transform.rotation.eulerAngles.x;
            while (Mathf.Abs(Mathf.DeltaAngle(CurrentRotation, TargetRotation)) > 0.1f)
            {
                CurrentRotation = Mathf.SmoothDampAngle(CurrentRotation, TargetRotation, ref duration, 0.5f);
                transform.rotation = Quaternion.Euler(CurrentRotation + 0.1f, StartRotation.eulerAngles.y, StartRotation.eulerAngles.z);
                yield return null;
            }
            if (Mathf.Abs(Mathf.DeltaAngle(CurrentRotation, TargetRotation)) > 0.1f)
            {
                yield break;
            }
        }
    }
}
