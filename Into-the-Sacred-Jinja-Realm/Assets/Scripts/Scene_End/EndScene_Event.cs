using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EndScene_Event : MonoBehaviour
{
    [Header("Fade Settings")]
    public Image Background;
    public Image Fade_Img;
    public TextMeshProUGUI Fade_Text;
    private FadeController fadeController;

    public Button backButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        fadeController = FindFirstObjectByType<FadeController>();

    }
    void Start()
    {
        if (Fade_Img != null && Fade_Text != null)
            StartCoroutine(fadeController.FadeOut(Fade_Img, Fade_Text, 3f));
        backButton.onClick.AddListener(BackToStartScene);
    }

    public void BackToStartScene()
    {
        if (Fade_Img != null && Fade_Text != null)
            StartCoroutine(fadeController.FadeIn(Background, Fade_Text, 3f, "Activity_Test"));
    }
}
