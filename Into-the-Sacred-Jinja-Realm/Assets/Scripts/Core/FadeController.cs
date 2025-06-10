using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{

    //Fade in only image
    public IEnumerator FadeInImage(Image targetImage, float duration)
    {
        if (targetImage.gameObject.activeSelf == false)
            targetImage.gameObject.SetActive(true);

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / duration); // Calculate alpha value
            UpdateAlpha(targetImage, null, alpha); // Update alpha value
            yield return null;
        }
        UpdateAlpha(targetImage, null, 1f);// Ensure alpha is set to 1
    }

    //Fade in only text
    public IEnumerator FadeInText(TextMeshProUGUI targetText, float duration)
    {
        if (targetText.gameObject.activeSelf == false)
            targetText.gameObject.SetActive(true);

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / duration); // Calculate alpha value
            UpdateAlpha(null, targetText, alpha); // Update alpha value
            yield return null;
        }
        UpdateAlpha(null, targetText, 1f);// Ensure alpha is set to 1
    }

    //Fade in without scene
    public IEnumerator FadeIn(Image targetImage, TextMeshProUGUI targetText, float duration)
    {

        if (targetImage.gameObject.activeSelf == false)
            targetImage.gameObject.SetActive(true);

        if (targetText.gameObject.activeSelf == false)
            targetText.gameObject.SetActive(true);

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / duration); // Calculate alpha value
            UpdateAlpha(targetImage, targetText, alpha); // Update alpha value
            yield return null;
        }
        UpdateAlpha(targetImage, targetText, 1f);// Ensure alpha is set to 1
    }
    
    //Fade in with scene
    public IEnumerator FadeIn(Image targetImage, TextMeshProUGUI targetText, float duration, String SceneName)
    {
        if (targetImage.gameObject.activeSelf == false)
            targetImage.gameObject.SetActive(true);

        if (targetText.gameObject.activeSelf == false)
            targetText.gameObject.SetActive(true);

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / duration); // Calculate alpha value
            UpdateAlpha(targetImage, targetText, alpha); // Update alpha value
            yield return null;
        }
        UpdateAlpha(targetImage, targetText, 1f);// Ensure alpha is set to 1

        SceneManager.LoadScene(SceneName); // load the next scene 
    }


    //Fade out only image
    public IEnumerator FadeOutImage(Image targetImage, float duration)
    {

        // Check if the fade image and text are active, if not, activate them
        if (targetImage.gameObject.activeSelf == false)
            targetImage.gameObject.SetActive(true);

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / duration); // Calculate alpha value
            UpdateAlpha(targetImage, null, alpha); // Update alpha value
            yield return null;
        }
        UpdateAlpha(targetImage, null, 0f);// Ensure alpha is set to 0

        targetImage.gameObject.SetActive(false); // disable image
    }

    //Fade out only text
    public IEnumerator FadeOutText(TextMeshProUGUI targetText, float duration)
    {
        if (targetText.gameObject.activeSelf == false)
            targetText.gameObject.SetActive(true);

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / duration); // Calculate alpha value
            UpdateAlpha(null, targetText, alpha); // Update alpha value
            yield return null;
        }
        UpdateAlpha(null, targetText, 0f);// Ensure alpha is set to 0

        targetText.gameObject.SetActive(false); // disable text
    }

    //Fade out 
    public IEnumerator FadeOut(Image targetImage, TextMeshProUGUI targetText, float duration)
    {
        // Check if the fade image and text are active, if not, activate them
        if (targetImage.gameObject.activeSelf == false)
            targetImage.gameObject.SetActive(true);

        if (targetText.gameObject.activeSelf == false)
            targetText.gameObject.SetActive(true);

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / duration); // Calculate alpha value
            UpdateAlpha(targetImage, targetText, alpha); // Update alpha value
            yield return null;
        }
        UpdateAlpha(targetImage, targetText, 0f);// Ensure alpha is set to 0

        targetImage.gameObject.SetActive(false); // disable image
        targetText.gameObject.SetActive(false); // disable text
    }


    // Update the alpha value of the fade image and text
    private void UpdateAlpha(Image targetImage, TextMeshProUGUI targetText, float alpha)
    {
        // Update the alpha value of the fade image and text
        if (targetImage != null)
        {
            Color imgColor = targetImage.color;
            imgColor.a = alpha;
            targetImage.color = imgColor;
        }

        if (targetText != null)
        {
            Color txtColor = targetText.color;
            txtColor.a = alpha;
            targetText.color = txtColor;
        }
    }
   
}
