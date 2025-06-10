using System;
using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public float ActionTime = 0f;
    public AudioSource[] AudioSource;

    [Header("Walking Audio")]
    public AudioSource WalkAudio;
    public AudioClip BridgeWalkClip;
    public AudioClip GroundWalkClip;
    public AudioClip StageWalkClip;

    [Header("Background Audio")]
    public AudioSource BackgroundAudio;
    public AudioClip BackgroundClip;

    [Header("Action State")]
    public bool IsAction = false;

    private GroundController groundController;
    private StageController stageController;
    private Vector3 LastPosition;
    private Coroutine LoopCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        groundController = FindFirstObjectByType<GroundController>();
        stageController = FindFirstObjectByType<StageController>();
        AudioSource = GetComponents<AudioSource>();
        WalkAudio = AudioSource[0];
        BackgroundAudio = AudioSource[1];
        WalkAudio.clip = BridgeWalkClip;
        BackgroundAudio.clip = BackgroundClip;
        BackgroundAudio.loop = true;
        BackgroundAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        float Movement = (transform.position - LastPosition).magnitude / Time.deltaTime;
        LastPosition = transform.position;
        if (Movement > 0.1f)
        {
            IsAction = true;
        }
        else
        {
            IsAction = false;
        }
        if (groundController.isGrounded)
        {
            WalkAudio.clip = GroundWalkClip;
        }
        else if (stageController.IsStage && !groundController.isGrounded)
        {
            WalkAudio.clip = StageWalkClip;
        }
        PlayWalkAudio(IsAction);
        BackgroundAudioLow(IsAction);
    }
    public void PlayWalkAudio(bool IsAction)
    {
        if (IsAction)
        {
            if (LoopCoroutine == null)
            {
                LoopCoroutine = StartCoroutine(AudioLoop(WalkAudio));
            }
        }
        else
        {
            if (LoopCoroutine != null)
            {
                StopCoroutine(LoopCoroutine);
                LoopCoroutine = null;
                WalkAudio.Stop();
            }
        }

    }
    IEnumerator AudioLoop(AudioSource Source)
    {
        while (true)
        {
            float Sagment = UnityEngine.Random.Range(2f, 4f);
            float Start = UnityEngine.Random.Range(0f, Source.clip.length - Sagment);
            Source.time = Start;
            Source.Play();
            yield return new WaitForSeconds(Sagment);
            Debug.Log("Audio Loop");
            Source.Stop();
        }
    }
    void BackgroundAudioLow(bool IsAction)
    {
        if (IsAction)
        {
            ActionTime += Time.deltaTime;
            ActionTime = Mathf.Clamp(ActionTime, 0f, 1f);
            BackgroundAudio.volume = Mathf.Lerp(1f, 0.2f, ActionTime);
        }
        else
        {
            ActionTime -= Time.deltaTime;
            ActionTime = Mathf.Clamp(ActionTime, 0f, 1f);
            BackgroundAudio.volume = Mathf.Lerp(1f, 0.2f, ActionTime);
        }
    }
}
