using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MoveCam : MonoBehaviour
{
    public bool PlaneFollow = false; // Flag to check if the scene is Scene1
    public GameObject MaskPlane;
    public float T = 0f; // Reset T to 0 when PlaneFollow is false
    public Material GateMaterial;
    public float Speed;// Flag to check if the scene is Scene1
    private Transform trans;
    private AudioSource GateAudioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GateAudioSource = MaskPlane.GetComponent<AudioSource>(); // Get the AudioSource component of the GameObject
        trans = GetComponent<Transform>(); // Get the Transform component of the GameObject
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // Trigger the animation when the space key is pressed
            trans.Translate(Vector3.forward * Time.deltaTime * Speed); // Move forward at a speed of 5 units per second
        }
        if (Input.GetKey(KeyCode.R))
        {
            // Trigger the animation when the space key is pressed
            trans.Translate(Vector3.up * Time.deltaTime * Speed); // Move forward at a speed of 5 units per second
        }
        if (Input.GetKey(KeyCode.F))
        {
            // Trigger the animation when the space key is pressed
            trans.Translate(Vector3.down * Time.deltaTime * Speed); // Move forward at a speed of 5 units per second
        }
        if (trans.position.x < -52f)
        {
            if (MaskPlane != null)
            {
                T += Time.deltaTime * 0.1f;
                T = Mathf.Clamp(T, 0f, 1f);// Reset T to 0 when PlaneFollow is false
                GateAudioSource.volume = Mathf.Lerp(1f, 0f, T * 2f);
                GateMaterial.SetColor("_MaskColor", new(0f, 0f, 0f));
                GateMaterial.SetFloat("_Alpha", Mathf.Lerp(1.5f, 0f, T));
                MaskPlane.transform.position = trans.position + trans.forward * 1f;
            }

        }

    }
    
}
