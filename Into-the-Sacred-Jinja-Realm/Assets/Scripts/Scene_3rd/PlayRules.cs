using UnityEngine;

public class PlayRules : MonoBehaviour
{
    IntroFade introFade;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            introFade = FindFirstObjectByType<IntroFade>();
            introFade.enabled = true;
        }
    }
}
