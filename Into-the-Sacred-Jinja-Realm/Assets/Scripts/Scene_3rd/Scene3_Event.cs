using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scene3_Event : MonoBehaviour
{
    public Image Fade_Img;
    public TextMeshProUGUI Fade_text;
    private FadeController fadeController;
    void Awake()
    {
        fadeController = FindFirstObjectByType<FadeController>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(fadeController.FadeOut(Fade_Img, Fade_text, 1f));
    }
}
