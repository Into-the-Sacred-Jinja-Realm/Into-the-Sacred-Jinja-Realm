using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Scene_1st_Event : MonoBehaviour
{

    [Header("Fade Settings")]
    public Image Fade_Img;
    public TextMeshProUGUI Fade_Text;

    [SerializeField]private FadeController fadeController;
    [SerializeField]private CameraMovement cameraMovement;
    [SerializeField]private Gestures_Listener gestures_Listener;


    private bool canmove = false;
    private bool isTrigger = false;
    

    void Start()
    {

        if (Fade_Img != null && Fade_Text != null)
            StartCoroutine(fadeController.FadeOut(Fade_Img, Fade_Text, 3f));
    }


    void Update()
    {
        //Request player need to raise both hands before moving
        if (!canmove)
        {
            canmove = gestures_Listener.IsRaisedLeftRaisedRightHand();
        }

        else if (canmove && !isTrigger)
        {
            isTrigger = true;
            cameraMovement.ChangeActive(0.5f);
        }

    }
}
