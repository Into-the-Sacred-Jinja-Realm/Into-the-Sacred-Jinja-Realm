using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Gate_Listener : MonoBehaviour
{
    public Material material;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material.SetColor("_MaskColor", Color.red);
        material.SetFloat("_Alpha", 0f);
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     if (material.GetFloat("_Alpha") < 0.1f)
    //     {
    //         Debug.Log("Bye");
    //         DestroyImmediate(gameObject);
    //     }
    // }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.LogWarning("Enter Gate Trigger");
            StartCoroutine(LoadNewScene("Scene_2nd"));
        }
        // 進入 Trigger 區域後就銷毀自己
    }

    IEnumerator LoadNewScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null; // Wait until the scene is fully loaded
        }

    }
}
