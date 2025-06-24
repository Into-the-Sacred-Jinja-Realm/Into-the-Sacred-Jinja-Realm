using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KamiAppear : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Scripts")]
    [SerializeField] private Gestures_Listener gestures_Listener;
    [SerializeField]private BloomController bloomController;
    [SerializeField]private FadeController fadeController;

    [Header("Kami Settings")]
    private GameObject[] Kami;
    private bool isKamiAppear = false;

    [Header("Fade Settings")]
    public Image Fade_Img;
    public TextMeshProUGUI Fade_text;

    void Awake()
    {
        Kami = GameObject.FindGameObjectsWithTag("Kami");
        if (Kami != null)
            foreach (GameObject kamiparent in Kami)
            {
                kamiparent.GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
            }
        else
            Debug.Log("Not Found Kami Parents");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKamiAppear && (Input.GetKeyDown(KeyCode.Alpha2) || gestures_Listener.IsHandClap()))
        {
            isKamiAppear = true;
            StartCoroutine(KamiAppearTime());
        }
        else if (isKamiAppear && (Input.GetKeyDown(KeyCode.Alpha3) || gestures_Listener.IsRaisedLeftRaisedRightHand()))
        {
            bloomController.IsKami = true;
            StartCoroutine(WaitTime(2f));
        }
        
    }
    private IEnumerator WaitTime(float duration)
    {
        
        yield return new WaitForSeconds(duration);
        Debug.Log("Goob bye");
        if (SceneManager.GetActiveScene().name == "Scene_3rd")
            StartCoroutine(fadeController.FadeIn(Fade_Img, Fade_text, 3f, "EndingScene"));
    }
    public IEnumerator KamiAppearTime()
    {
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            foreach (GameObject kamiparent in Kami)
            {
                kamiparent.GetComponent<Renderer>().material.SetFloat("_Alpha", Mathf.Lerp(0f, 1f, timer));
            }
            yield return null;
            if (timer >= 1f)
            {
                timer = 1f;
                break;
            }
        }
    }

    public IEnumerator KamiDisappearTime()
    {
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            foreach (GameObject kamiparent in Kami)
            {
                kamiparent.GetComponent<Renderer>().material.SetFloat("_Alpha", Mathf.Lerp(1f, 0f, timer));
            }
            yield return null;
            if (timer >= 1f)
            {
                timer = 1f;
                break;
            }
        }
        this.enabled = false;
    }
}
