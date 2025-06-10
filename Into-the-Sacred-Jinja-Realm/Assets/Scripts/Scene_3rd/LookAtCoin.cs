using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LookAtCoin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public IEnumerator LookAtTarget(Animator Animator, string Step, GameObject TargetObject)
    {
        Quaternion startRotation = transform.rotation; // Store the initial rotation
        while (Animator.GetCurrentAnimatorStateInfo(0).IsName(Step) && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
                        Debug.Log("Target Rotation: Hi");
            Vector3 direction = TargetObject.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Debug.Log("Target Rotation: " + targetRotation.eulerAngles);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
            yield return null;
        }
        if (Animator.GetCurrentAnimatorStateInfo(0).IsName(Step) && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, Time.deltaTime * 2f);
        }// Exit the loop when the animation is complete
    }
}
