using System.Collections.Generic;
using UnityEngine;

public class ToriiAndGateController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Material GateMaterial; // Material for the gate // Material for the gate
    public Material[] ToriiMaterial;// List to hold the materials for the Torii
    public GameObject Gate;
    private AudioSource GateAudioSource; // Reference to the Move script
    void Start()
    {
        GateAudioSource = Gate.GetComponent<AudioSource>();
        GateMaterial.SetFloat("_Alpha", 0f);
        GateMaterial.SetFloat("_EffectStrength", 0f);
        for (int i = 0; i < ToriiMaterial.Length; i++)
        {
            ToriiMaterial[i].SetFloat("_Alpha", 0f); // Set the initial alpha value of the Torii materials to 0
        }
        // Set all Value of materials to 0
    }
    public void ToriiController(float T)
    {
        Debug.Log("Stage Torii");
        float ToriiAlphaValue = 0f;
        for(int i = 0; i < ToriiMaterial.Length; i++)
        {
            ToriiAlphaValue = Mathf.Lerp(0f, 1.5f, T);
            ToriiMaterial[i].SetFloat("_Alpha", ToriiAlphaValue);
        }
    }
    public void GateController(float T)
    {
        GateAudioSource.volume = Mathf.Lerp(0f, 1f, T); ; // Reset the volume of the audio source
        float GateAlphaValue = Mathf.Lerp(0f, 1.5f, T);
        float GateEffectStrength = Mathf.Lerp(0f, 1f, T);
        GateMaterial.SetFloat("_Alpha", GateAlphaValue);
        GateMaterial.SetFloat("_EffectStrength", GateEffectStrength);
    }
}
