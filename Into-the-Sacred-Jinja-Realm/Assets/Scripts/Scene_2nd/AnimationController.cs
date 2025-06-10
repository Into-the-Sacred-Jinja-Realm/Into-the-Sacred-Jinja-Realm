using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    public Animator Animator;
    public Transform Player;
    private Camera PlayerCamera; // Reference to the camera component
    public GameObject LookTarget;// Reference to the player object
    public Material HandsMaterial;
    public GameObject Water;

    [Header("Animation Steps")]
    public bool Step1 = false;
    public bool Step2 = false;
    public bool Step3 = false;
    public bool Step4 = false;

    private TextureController[] TextureController; // Default scale value
    private Gestures_Listener gestures_Listener;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HandsMaterial.SetFloat("_Alpha", 1f);
        Animator = GetComponent<Animator>();
        PlayerCamera = Player.GetComponent<Camera>(); // Get the camera component from the player
        TextureController = FindObjectsByType<TextureController>(FindObjectsSortMode.None); // Get the TextureController component
        gestures_Listener = FindFirstObjectByType<Gestures_Listener>();
        Water.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Step1 && (gestures_Listener.IsRaisedLeftRaisedRightHand()|| Input.GetKeyDown(KeyCode.Space)))
            PlayAnimatior(1);//turn1
        if (!Step2 && (gestures_Listener.IsRaisedRightHand() || Input.GetKeyDown(KeyCode.Z))
        && Animator.GetCurrentAnimatorStateInfo(0).IsName("Scene")
        && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Water.SetActive(true);
            PlayAnimatior(2);//turn2
        }
            
        if (!Step3 && (gestures_Listener.IsRaisedLeftHand()|| Input.GetKeyDown(KeyCode.X))
        && Animator.GetCurrentAnimatorStateInfo(0).IsName("Scene 1")
        && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            PlayAnimatior(3);//turn3
        if (!Step4 && (gestures_Listener.IsHandTogerther()|| Input.GetKeyDown(KeyCode.C))
        && Animator.GetCurrentAnimatorStateInfo(0).IsName("Scene 2")
        && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            PlayAnimatior(4);//turn4
        AnimatorPlayer("Step1", "Scene", Step1);

        AnimatorPlayer("Step2", "Scene 1", Step2);

        AnimatorPlayer("Step3", "Scene 2", Step3);

        AnimatorPlayer("Step4", "Scene 3", Step4);

    }

    public void PlayAnimatior(int num)
    {
        switch (num)
        {
            case 1:

                Step1 = true;
                break;

            case 2:

                Step2 = true;
                break;

            case 3:
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                Step3 = true;
                break;

            case 4:
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                Step4 = true;
                break;

        }

    }
    private void AnimatorPlayer(string Step, string Scene, bool StepOn)//-----AnimatorPlayer-----
    {
        if (StepOn)
        {
            if (Animator.GetCurrentAnimatorStateInfo(0).IsName(Scene) && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f)
            {
                Player.LookAt(LookTarget.transform.position);
                PlayerCamera.fieldOfView = 120f;
            }
            foreach (TextureController textureController in TextureController)
            {
                textureController.IsAnimationPlaying = true;
            }
            for (int i = 0; i < TextureController.Length; i++)
            {
                if (TextureController[i].TextureScale >= 0.9f)
                {
                    Animator.SetTrigger(Step);
                }
            }
            if (Animator.GetCurrentAnimatorStateInfo(0).IsName(Scene) && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                foreach (TextureController textureController in TextureController)
                {
                    textureController.IsAnimationPlaying = false;
                    Player.LookAt(null);
                    PlayerCamera.fieldOfView = 80f;
                    Player.rotation = Quaternion.Slerp(Player.rotation, Quaternion.Euler(45f, -90f, 0f), 1.0f); // Reset camera rotation
                }
            }
        }
    }
}
