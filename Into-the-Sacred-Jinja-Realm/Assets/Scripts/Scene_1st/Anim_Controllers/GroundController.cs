using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundController : MonoBehaviour
{
    [Header("Ground Animator")]
    public Animator GroundAnimator;
    private CameraMovement[] CameraMovements;

    [Header("State")]
    //need to check
    public bool isGrounded = false;

    [Header("Audio Settings")]
    private AudioSource StoneAudioSource;

    [Header("Objects")]
    public GameObject GroundOb;
    public GameObject StageOb;
    // Reference to the audio source for stone sound
    private void Awake()
    {
        StoneAudioSource = GetComponent<AudioSource>();
        CameraMovements = FindObjectsByType<CameraMovement>(FindObjectsSortMode.None);
    }
    private void OnTriggerEnter(Collider other)
    { 
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
