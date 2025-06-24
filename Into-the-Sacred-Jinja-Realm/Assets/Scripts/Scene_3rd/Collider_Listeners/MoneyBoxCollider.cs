using System.Collections;
using UnityEngine;

public class MoneyBoxCollider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject PlayerHands;
    public GameObject Coin;
    [SerializeField]private CameraMovement CameraMovement;
    [SerializeField]private BloomController BloomController;
    [SerializeField]private KamiAppear kamiAppear;
    private Animator playerAnim;

    public GameObject CheckPoint;

    void Start()
    {
        playerAnim = PlayerHands.GetComponent<Animator>();
        StartCoroutine(kamiAppear.KamiDisappearTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ThrowCoin();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPoint.SetActive(false);
            CameraMovement.ChangeActive();
        }
    }

    private IEnumerator AfterMoneyBox()
    {
        yield return new WaitForSeconds(0.5f);
        while (PlayerHands.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Step2") && PlayerHands.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime != 1f)
        {
            if (PlayerHands.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Step2") && PlayerHands.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                BloomController.IsMoneyBox = true;
                kamiAppear.enabled = true;
                break;
            }
            yield return null;
        }
    }
    
    public void ThrowCoin()
    {
        playerAnim.SetBool("GoToStep2", true);
        StartCoroutine(AfterMoneyBox());
    }
}
