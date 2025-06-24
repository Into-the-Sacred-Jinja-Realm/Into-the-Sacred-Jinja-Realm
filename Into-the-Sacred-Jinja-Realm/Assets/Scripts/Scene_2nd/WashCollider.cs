using System.Collections;
using UnityEngine;

public class WashCollider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]private Scene2CamRotate Scene2CamRotate;
    [SerializeField]private CameraMovement CameraMovement;
    [SerializeField]private AnimationController animationController;
    [SerializeField]private IntroFade introFade;
    public GameObject CheckPoint;

    void Start()
    {
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
    
    public void PlayAnimation()
    {
        if (animationController != null)
            animationController.enabled = true;
        else
            Debug.Log("animationController not found");
    }
}
