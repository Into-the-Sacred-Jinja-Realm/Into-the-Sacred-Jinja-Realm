using UnityEngine;

public class KamiAction : MonoBehaviour
{
    public GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if (GetComponent<Renderer>().material.GetFloat("_Alpha") < 1f)
            {
                Debug.LogWarning("Fuck");
                float Distance = Mathf.Abs(Player.transform.position.x - transform.position.x);
                GetComponent<Renderer>().material.SetFloat("_Alpha", 1 - Distance / 400);
            }
    }
}
