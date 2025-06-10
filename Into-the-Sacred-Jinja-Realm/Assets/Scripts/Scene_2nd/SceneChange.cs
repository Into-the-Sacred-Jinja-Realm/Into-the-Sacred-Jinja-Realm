using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{

    public Image FadeImage;
    private bool isChanging = false;

    private AfterWash AfterWash;
    private FadeController fadeController;

    void Start()
    {
        AfterWash = FindFirstObjectByType<AfterWash>();
        fadeController = FindFirstObjectByType<FadeController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isChanging && AfterWash.TextureScale <= 0f)
        {
            isChanging = true;
            StartCoroutine(FadeAndLoadScene());
        }
        
    }

    private IEnumerator FadeAndLoadScene()
    {
        yield return StartCoroutine(fadeController.FadeInImage(FadeImage, 2f));
        yield return StartCoroutine(LoadNewScene("Scene_3rd"));
    }
    private IEnumerator LoadNewScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null; // Wait until the scene is fully loaded
        }

    }
}
