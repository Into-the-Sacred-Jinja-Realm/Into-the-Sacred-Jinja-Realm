using UnityEngine;

public class HandsFollowPlayer_Scene3 : MonoBehaviour
{
    public Transform PlayerTransform; // Reference to the player's transform
    private Quaternion StartRotation; // Store the initial rotation of the hands
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(PlayerTransform.position.x + 85f, PlayerTransform.position.y - 5f, PlayerTransform.position.z + 1.75f);
        transform.rotation = Quaternion.Euler(StartRotation.eulerAngles.x, StartRotation.eulerAngles.y, StartRotation.eulerAngles.z);
    }
}
