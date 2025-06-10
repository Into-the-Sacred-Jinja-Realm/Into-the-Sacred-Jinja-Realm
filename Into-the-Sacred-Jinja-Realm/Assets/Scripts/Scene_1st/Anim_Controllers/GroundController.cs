using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundController : MonoBehaviour
{
    [Header("Ground Animator")]
    public Animator GroundAnimator;

    [Header("State")]
    public bool isGrounded = false;

    [Header("Audio Settings")]
    public AudioSource StoneAudioSource;

    [Header("Objects")]
    public GameObject GroundOb;
    public GameObject StageOb;
     // Reference to the audio source for stone sound
    private void Awake()
    {
        StoneAudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        var CameraMovements = FindObjectsByType<CameraMovement>(FindObjectsSortMode.None);
        if (other.CompareTag("Player"))
        {
            GroundOb.SetActive(false);
            StageOb.SetActive(true);
            isGrounded = true;
            GroundAnimator.SetTrigger("Start");
            foreach (var CameraMovement in CameraMovements)
            {
                if (CameraMovement != null)
                {
                    CameraMovement.ChangeActive(7f);
                }
            }
        }
    }
}
