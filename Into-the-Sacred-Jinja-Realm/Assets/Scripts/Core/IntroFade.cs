using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(FadeController))]
public class IntroFade : MonoBehaviour
{
    private FadeController fadeController;
    [SerializeField]private WashCollider washCollider;
    bool isFirst = true;
    [Header("UI")]
    public Image BackGroundImg;
    public Image StartImage;
    public TextMeshProUGUI StartImgText;
    public TextMeshProUGUI StartText;
    [Header("Others UI")]
    public Image OtherImage;
    public TextMeshProUGUI OtherImgText;
    public TextMeshProUGUI[] OtherText;
    public Sprite[] Gesture_sprites;

    [Header("Settings")]
    TextAsset textAsset;
    private string[] IntroText;
    public Text calibrationText;
    public TextMeshProUGUI debug_Text;


    void Start()
    {
        fadeController = GetComponent<FadeController>();
        ReloadIntroText();
        if (calibrationText != null && debug_Text != null)
        {
            calibrationText.enabled = false;
            debug_Text.enabled = false;
        }

        StartCoroutine(PlayIntro());
    }


    private void ReloadIntroText()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Scene_1st":
                TextSet("Scene1_IntroText");
                break;

            case "Scene_2nd":
                TextSet("Scene2_IntroText");
                break;

            case "Scene_3rd":
                TextSet("Scene3_IntroText");
                break;

            default:
                Debug.Log("Scene not found");
                break;
        }
    }


    private void TextSet(string fileName)
    {
        textAsset = Resources.Load<TextAsset>("IntroText/" + fileName);
        IntroText = textAsset.text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
    }

    private IEnumerator PlayIntro()
    {
        if (StartImage != null && StartImgText != null && BackGroundImg != null)
            if (isFirst)
            {
                StartImgText.text = IntroText[0];
                Turnon(StartImage, StartImgText, BackGroundImg, null);
                yield return new WaitForSeconds(3f);
                Turnoff(null, null, null, StartImgText);
                isFirst = false;
                yield return new WaitForSeconds(3f);
            }
        for (int i = 0; i < Gesture_sprites.Length; i++)
        {
            if (i > 0)
            {
                Turnoff(OtherImage, OtherImgText, null, null);
                yield return new WaitForSeconds(3f);
            }


            OtherImage.sprite = Gesture_sprites[i];
            OtherImgText.text = IntroText[i + 1];
            Turnon(OtherImage, OtherImgText, null, null);
            SetAnimatorControllerFromSprite(Gesture_sprites[i]);
            yield return new WaitForSeconds(3f);
        }

        Turnoff(OtherImage, OtherImgText, BackGroundImg, null);
        yield return new WaitForSeconds(3f);
        if (calibrationText != null && debug_Text != null)
        {
            calibrationText.enabled = true;
            debug_Text.enabled = true;
        }
        if (SceneManager.GetActiveScene().name == "Scene_2nd")
        {
            washCollider.PlayAnimation();
        }
        this.enabled = false;

    }
    
    private void Turnon(Image Image, TextMeshProUGUI Text, Image SecImage, TextMeshProUGUI SecText)
    {
        if (Image != null && Text != null)
            StartCoroutine(fadeController.FadeIn(Image, Text, 1f));

        if (SecImage != null)
            StartCoroutine(fadeController.FadeInImage(SecImage, 1f));

        if (SecText != null)
            StartCoroutine(fadeController.FadeInText(SecText, 1f));

    }
    private void Turnoff(Image Image, TextMeshProUGUI Text, Image SecImage, TextMeshProUGUI SecText)
    {
        if (Image != null && Text != null)
            StartCoroutine(fadeController.FadeOut(Image, Text, 1f));

        if (SecImage != null)
            StartCoroutine(fadeController.FadeOutImage(SecImage, 1f));

        if (SecText != null)
            StartCoroutine(fadeController.FadeOutText(SecText, 1f));
    }

    public void SetAnimatorControllerFromSprite(Sprite sprite)
    {
        string baseName = sprite.name;
        int lastUnderscore = baseName.LastIndexOf('_');
        if (lastUnderscore != -1 && int.TryParse(baseName[(lastUnderscore + 1)..], out _))
        {
            baseName = baseName[..lastUnderscore];
        }

        string controllerPath = $"Animators/{baseName}_Animator";


        RuntimeAnimatorController controller = Resources.Load<RuntimeAnimatorController>(controllerPath);

        if (controller != null)
        {
            OtherImage.GetComponent<Animator>().runtimeAnimatorController = controller;
        }
        else
        {
            Debug.LogError($"Animator Controller not found at path: Resources/{controllerPath}");
        }
    }
}
