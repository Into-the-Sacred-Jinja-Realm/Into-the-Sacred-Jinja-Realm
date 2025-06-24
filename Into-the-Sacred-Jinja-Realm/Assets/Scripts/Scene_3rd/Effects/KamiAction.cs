using UnityEngine;

public class KamiAction : MonoBehaviour
{
    public GameObject Player;
    // Update is called once per frame
    void Update()
    {
            if (GetComponent<Renderer>().material.GetFloat("_Alpha") < 1f)
            {
                float Distance = Mathf.Abs(Player.transform.position.x - transform.position.x);
                GetComponent<Renderer>().material.SetFloat("_Alpha", 1 - Distance / 400);
            }
    }
}
