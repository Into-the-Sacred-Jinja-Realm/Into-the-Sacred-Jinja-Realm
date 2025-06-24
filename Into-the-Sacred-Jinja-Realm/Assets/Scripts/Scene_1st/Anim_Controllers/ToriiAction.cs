using UnityEngine;

public class ToriiAction : MonoBehaviour
{

    [Header("Player")]
    public GameObject Player;

    private float ActionTime = 0f;
    [SerializeField]private GroundController groundController;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        GetComponent<Renderer>().material.SetFloat("_Alpha", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (groundController.GroundAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.2f && groundController.GroundAnimator.GetCurrentAnimatorStateInfo(0).IsName("GroundAnimation"))
        {
            Debug.LogWarning("GroundAnimation is finished");
            if (GetComponent<Renderer>().material.GetFloat("_Alpha") < 1f)
            {
                float Distance = Player.transform.position.x - transform.position.x;
                if (Distance < 40f)
                {
                    ActionTime += Time.deltaTime;
                    ActionTime = Mathf.Clamp(ActionTime, 0f, 1f);
                    GetComponent<Renderer>().material.SetFloat("_Alpha", Mathf.Lerp(0f, 1f, ActionTime));
                }
            }
        }
    }
}
