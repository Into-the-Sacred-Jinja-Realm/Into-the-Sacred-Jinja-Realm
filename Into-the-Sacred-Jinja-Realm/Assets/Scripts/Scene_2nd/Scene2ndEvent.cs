using UnityEngine;

public class Scene2ndEvent : MonoBehaviour
{
    public GameObject MaskPlane;
    public float timer = 0f; // Reset timer to 0 when PlaneFollow is false
    private Transform trans;
    public Material GateMaterial;
    private AudioSource GateAudioSource;
    private bool isSayBye = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GateAudioSource = MaskPlane.GetComponent<AudioSource>();
        trans = GetComponent<Transform>();
        GateMaterial.SetColor("_MaskColor", Color.red);
        GateMaterial.SetFloat("_Alpha", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * 0.1f; // Reset timer to 0 when PlaneFollow is false
        timer = Mathf.Clamp(timer, 0f, 1f); // Clamp timer to the range [0, 1]
        if (MaskPlane != null)
        GateController(timer);
        
        if (GateMaterial.GetFloat("_Alpha") < 0.1f)
        {
            if(!isSayBye)
            {
                isSayBye = true;
                Debug.Log("Bye");
            }
            
            DestroyImmediate(MaskPlane);
        }
    }

    void GateController(float T)
    {   
        GateAudioSource.volume = Mathf.Lerp(1f, 0f, T * 2f);
        GateMaterial.SetColor("_MaskColor", new(0f, 0f, 0f));
        GateMaterial.SetFloat("_Alpha", Mathf.Lerp(1.5f, 0f, T));
        MaskPlane.transform.position = transform.position + transform.forward * 1f;
    }
}
