using System.Collections;
using UnityEngine;

public class GetMoneyCollider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject PlayerHands;
    public GameObject Coin;
    public GameObject[] Hands;
    private Animator playerAnim;

    public GameObject[] CheckPoint;

    [SerializeField]private CameraMovement CameraMovement;
    [SerializeField] private udp_receive_only udp_Receive;
    void Start()
    {
        playerAnim = PlayerHands.GetComponent<Animator>();
        foreach (GameObject hand in Hands)
        {
            hand.GetComponent<Renderer>().enabled = false;
        }
        
        udp_Receive.enabled = false;
        CheckPoint[1].SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        foreach (GameObject hand in Hands)
        {
            hand.GetComponent<Renderer>().enabled = true;
        }
        // Check if the collider that entered is the player
        if (other.CompareTag("Player"))
        {
            udp_Receive.enabled = true;

            playerAnim.SetBool("GoToStep1", true);

            CheckPoint[0].SetActive(false);
            CheckPoint[1].SetActive(true);

            CameraMovement.ChangeActive(2f);
        }

    }
}
