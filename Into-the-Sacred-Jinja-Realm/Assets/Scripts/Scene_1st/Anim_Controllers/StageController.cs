using JetBrains.Annotations;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [Header("Stage Animator")]
    public Animator StageAnimator;
    private ToriiAndGateController ToriiAndGateController;
    public float GateTime = 0f;
    public float ToriiTime = 0f;

    [Header("State")]
    public bool IsStage = false;
    public bool IsGateOpen = false;

    private GroundController GroundControll;

    [Header("Audio Settings")]
    public AudioSource StoneAudioSource;

    public GameObject StageOb;
    // Reference to the audio source for stone sound
    // Flag to indicate if this is a stage
    private void Awake()
    {
        ToriiAndGateController = GetComponent<ToriiAndGateController>();
        GroundControll = FindFirstObjectByType<GroundController>();
        StoneAudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (!IsGateOpen)
        {
            AfterEffect();
        }
    }
    void AfterEffect()
    {
            if (StageAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f && StageAnimator.GetCurrentAnimatorStateInfo(0).IsName("StageAnimation"))
            {
                ToriiTime += Time.deltaTime * 0.25f;
                ToriiTime = Mathf.Clamp(ToriiTime, 0f, 1f);
                ToriiAndGateController.ToriiController(ToriiTime);
                Debug.LogWarning("ToriiTime");
            }
            if (ToriiTime > 0.5f)
            {
                GateTime += Time.deltaTime * 0.25f; // Increment GateTime based on delta time
                GateTime = Mathf.Clamp(GateTime, 0f, 1f);
                ToriiAndGateController.GateController(GateTime);
                Debug.LogWarning("GateTime"); // Debug log for GateTime
            }
            if (GateTime >= 1f)
            {
                IsGateOpen = true; // Reset the stage flag
            }
    }
    private void OnTriggerEnter(Collider other)
    {
        var CameraMovements = FindObjectsByType<CameraMovement>(FindObjectsSortMode.None);
        if (other.CompareTag("Player"))
        {
            StageOb.GetComponent<Renderer>().enabled = false;
            GroundControll.isGrounded = false; // Reset the grounded state
            IsStage = true;
            StageAnimator.SetTrigger("Start");
            foreach (var CameraMovement in CameraMovements)
            {
                if (CameraMovement != null)
                {
                    CameraMovement.ChangeActive(5f);
                }
            }
        }
    }
}
