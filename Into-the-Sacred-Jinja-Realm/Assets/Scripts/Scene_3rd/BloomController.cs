using UnityEngine;

public class BloomController : MonoBehaviour
{
    [Header("Money Box Bloom Settings")]
    public GameObject[] MoneyBox;
    public float BIntensity = 0f;
    public bool IsMoneyBox = false;

    [Header("Janja Bloom Settings")]
    public GameObject[] Janja;
    public float JIntensity = 0f;
    public bool IsJanja = false;

    [Header("Kami Bloom Settings")]
    public GameObject[] Kami;
    public float KIntensity = 0f;
    public bool IsKami = false;


    // Adjust the bloom intensity as needed
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoneyBox = GameObject.FindGameObjectsWithTag("Box");
        Janja = GameObject.FindGameObjectsWithTag("Janja");
        Kami = GameObject.FindGameObjectsWithTag("Kami");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            IsMoneyBox = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            IsJanja = true;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            IsKami = true;
        }


        (IsMoneyBox, BIntensity) = IntensityCount(IsMoneyBox, BIntensity);
        (IsJanja, JIntensity) = IntensityCount(IsJanja, JIntensity);
        (IsKami, KIntensity) = IntensityCount(IsKami, KIntensity);


        MoneyBoxBloom(BIntensity);
        JanjaBloom(JIntensity);
        KamiBloom(KIntensity);
    }
    (bool, float) IntensityCount(bool IsActive, float Intensity)// ---------Count the intensity of bloom effect-------
    {
        if (IsActive)
        {
            Intensity += Time.deltaTime;
            if (Intensity >= 1f)
            {
                Intensity = 1f;
                IsActive = false;
            }
        }
        else
        {
            Intensity -= Time.deltaTime;
            if (Intensity <= 0f)
            {
                Intensity = 0f;
            }
        }
        return (IsActive, Intensity);
    }
    void MoneyBoxBloom(float Intensity)// ---------Bloom effect for Money Box-------
    {
        for (int i = 0; i < MoneyBox.Length; i++)
        {
            MoneyBox[i].GetComponent<Renderer>().material.SetFloat("_Intensity", Mathf.Lerp(0.5f, 250f, Intensity));
        }
    }
    void JanjaBloom(float Intensity)// ---------Bloom effect for Janja-------
    {
        for (int i = 0; i < Janja.Length; i++)
        {
            Janja[i].GetComponent<Renderer>().material.SetFloat("_Intensity", Mathf.Lerp(0.5f, 150f, Intensity));
        }
    }
    void KamiBloom(float Intensity)// ---------Bloom effect for Kami-------
    {
        for (int i = 0; i < Kami.Length; i++)
        {
            Kami[i].GetComponent<Renderer>().material.SetFloat("_Intensity", Mathf.Lerp(0.5f, 100f, Intensity));
        }
    }

    public void TriggerMoneyBoxBloom()
    {
        IsMoneyBox = true;
    }
}

