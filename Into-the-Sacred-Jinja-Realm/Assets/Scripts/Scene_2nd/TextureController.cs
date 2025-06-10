using UnityEngine;

public class TextureController : MonoBehaviour
{
    public float TextureScale = 0f; 
    
    public bool IsAnimationPlaying = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextureScale = 0f; // Initialize the texture scale to 0
        GetComponent<Renderer>().material.SetFloat("_TextureScale", TextureScale); // Reset texture scale in material
    }

    // Update is called once per frame
    void Update()
    {
        TextureScale = UpdateTextureScale(IsAnimationPlaying, TextureScale);
    }
    float UpdateTextureScale(bool IsActive, float textureScale)
    {
        if (IsAnimationPlaying)
        {
            TextureScale += Time.deltaTime * 0.5f;
            TextureScale = Mathf.Clamp(TextureScale, 0f, 1f);
            if (gameObject.CompareTag("Water"))
            {
                GetComponent<Renderer>().material.SetFloat("_Smoothness", TextureScale * 2f);
                GetComponent<Renderer>().material.SetFloat("_Alpha", TextureScale);
            }
            else
            {
                GetComponent<Renderer>().material.SetFloat("_TextureScale", TextureScale);
            }
        }
        else
        {
            TextureScale -= Time.deltaTime * 0.5f; // Increment the texture scale over time
            TextureScale = Mathf.Clamp(TextureScale, 0f, 1f);// Reset texture scale when not playing animation
            if (gameObject.CompareTag("Water"))
            {
                GetComponent<Renderer>().material.SetFloat("_Smoothness", TextureScale * 2f);
                GetComponent<Renderer>().material.SetFloat("_Alpha", TextureScale);
            }
            else
            {
                GetComponent<Renderer>().material.SetFloat("_TextureScale", TextureScale);
            }
        }
        return (TextureScale);
    }
}
