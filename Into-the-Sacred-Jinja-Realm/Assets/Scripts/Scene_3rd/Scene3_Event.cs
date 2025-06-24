using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scene3_Event : MonoBehaviour
{
    public Image Fade_Img;
    public TextMeshProUGUI Fade_text;
    [SerializeField]private FadeController fadeController;
   
    void Start()
    {
        StartCoroutine(fadeController.FadeOut(Fade_Img, Fade_text, 1f));
    }
}
