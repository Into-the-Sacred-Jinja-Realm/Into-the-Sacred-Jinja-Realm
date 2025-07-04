using UnityEngine;

public class AfterWash : MonoBehaviour
{
    public Animator Animator;
    public float TextureScale = 1f; // Default scale value

    // Update is called once per frame
    void Update()
    {
        if(Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && Animator.GetCurrentAnimatorStateInfo(0).IsName("Scene 3"))
        {
            TextureScale -= Time.deltaTime * 0.5f;
            TextureScale = Mathf.Clamp(TextureScale, 0f, 1f); // Increment the texture scale over time
            GetComponent<Renderer>().material.SetFloat("_TextureScale", TextureScale); // Disable the renderer to hide the object
        }
    }
}
