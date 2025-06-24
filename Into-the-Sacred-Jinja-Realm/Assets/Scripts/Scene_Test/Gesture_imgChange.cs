using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gesture_imgChange : MonoBehaviour
{
    [Header("Settings")]
    public Image Gesture_image;
    public Sprite[] sprites;
    [SerializeField]private FadeController fadeController;
    private int num = 0;
    string[] spriteNames = { "RaiseLeftHand", "RaiseRightHand", "RaisedLeftRaisedRightHand", "HandTogerther" };

    [Header("UI")]
    public Image Fade_Img;
    public Image Success_Screen;
    public TextMeshProUGUI Fade_Text;
    public TextMeshProUGUI Success_Text;

    void Start()
    {
        Gesture_image.sprite = sprites[0];
    }
    
      
    public void ChangeImage(string gestureName)
    {
        //if the gesture value is over the length of the sprite array, reset it to 0 and go to the next scene
        if (num >= sprites.Length - 1)
        {
            num = 0;
            SetAnimatorControllerFromSprite(sprites[num]);
            StartCoroutine(fadeController.FadeIn(Fade_Img, Fade_Text, 3f, "Scene_1st"));
        }
        //Change the image if the gesture value equal sprite names
        else if (gestureName == spriteNames[num])
        {
            StartCoroutine(fadeController.FadeOut(Success_Screen, Success_Text, 1f));
            num++;
            SetAnimatorControllerFromSprite(sprites[num]);
        }
    }

    public void SetAnimatorControllerFromSprite(Sprite sprite)
    {
        string baseName = sprite.name;
        int lastUnderscore = baseName.LastIndexOf('_');
        //if the sprite name has a number at the end, remove it
        if (lastUnderscore != -1 && int.TryParse(baseName[(lastUnderscore + 1)..], out _))
        {
            baseName = baseName[..lastUnderscore];
        }

        string controllerPath = $"Animators/{baseName}_Animator";


        RuntimeAnimatorController controller = Resources.Load<RuntimeAnimatorController>(controllerPath);

        if (controller != null)
        {
            Gesture_image.GetComponent<Animator>().runtimeAnimatorController = controller;
        }
        else
        {
            Debug.LogError($"Animator Controller not found at path: Resources/{controllerPath}");
        }
    }
}
