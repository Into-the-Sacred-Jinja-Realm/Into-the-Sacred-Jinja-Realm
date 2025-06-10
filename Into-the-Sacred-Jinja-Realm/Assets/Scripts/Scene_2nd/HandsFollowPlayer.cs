using UnityEngine;

public class HandsFollowPlayer : MonoBehaviour
{
    public Transform PlayerTransform;
    private Quaternion StartRotation; // Reference to the player's transform
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartRotation = transform.rotation; // Store the initial rotation of the hands
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<AnimationController>().Step3 && !GetComponent<AnimationController>().Step4)
        {
            transform.position = new Vector3(PlayerTransform.position.x - 3f, PlayerTransform.position.y - 3f, PlayerTransform.position.z - 3.5f);
            transform.rotation = Quaternion.Euler(StartRotation.eulerAngles.x, StartRotation.eulerAngles.y, StartRotation.eulerAngles.z);
        }
        else if (GetComponent<AnimationController>().Step4)
        {
            transform.position = new Vector3(PlayerTransform.position.x - 3f, PlayerTransform.position.y - 3f, PlayerTransform.position.z + 1.5f);
            transform.rotation = Quaternion.Euler(StartRotation.eulerAngles.x, StartRotation.eulerAngles.y - 3.5f, StartRotation.eulerAngles.z);
        }
        else
        {
            transform.position = new Vector3(PlayerTransform.position.x - 3f, PlayerTransform.position.y - 3f, PlayerTransform.position.z + 1.75f);
            transform.rotation = Quaternion.Euler(StartRotation.eulerAngles.x, StartRotation.eulerAngles.y, StartRotation.eulerAngles.z);
        }
    }
}
