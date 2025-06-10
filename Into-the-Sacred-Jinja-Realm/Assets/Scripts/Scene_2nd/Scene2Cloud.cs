using UnityEngine;

public class Scene2Cloud : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Renderer>().material.SetFloat("_AlphaClip", 0.1f);
    }
}
