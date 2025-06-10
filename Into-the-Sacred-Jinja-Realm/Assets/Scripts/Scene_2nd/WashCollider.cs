using System.Collections;
using UnityEngine;

public class WashCollider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Scene2CamRotate Scene2CamRotate;
    private CameraMovement CameraMovement;
    private AnimationController animationController;
    private IntroFade introFade;
    public GameObject CheckPoint;

    void Start()
    {
        animationController = FindFirstObjectByType<AnimationController>();
        Scene2CamRotate = FindFirstObjectByType<Scene2CamRotate>();
        CameraMovement = FindFirstObjectByType<CameraMovement>();
        introFade = FindFirstObjectByType<IntroFade>();
        introFade.enabled = false;
        animationController.enabled = false;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPoint.SetActive(false);
            StartCoroutine(Scene2CamRotate.CamRotate(true));
            CameraMovement.ChangeActive();
            introFade.enabled = true;
            
        }

    }

    public void anima()
    {
        animationController.enabled = true;
    }
}
