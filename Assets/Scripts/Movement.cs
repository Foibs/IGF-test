using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    CharacterController playerController;
    public float speed = 10f;
    private Vector3 moving;
    public GameObject handle;
    public bool canChop;
    BoxCollider weapoonCollider;
    public GameObject axe;
    public float woodAmount = 0;
    public GameObject woodMax;
    public GameObject diposited;
    public GameObject maxedWood;
    public GameObject coins;
    public int coinNumber = 0;
    public GameObject message;

    private void Start()
    {
        playerController = GetComponent<CharacterController>();
        moving = Vector3.zero;
        canChop = true;
        weapoonCollider = axe.GetComponent<BoxCollider>();
        weapoonCollider.enabled = false;
        woodMax.SetActive(false);
        diposited.SetActive(false);
        maxedWood.SetActive(false);
        coins.GetComponent<Text>().text = "" + coinNumber;
        message.SetActive(false);
    }

    void Update()
    {
        Move();
        Rotate();

        if (Input.GetMouseButtonDown(0) && canChop == true)
        {
            Axe();
        }
    }

    private void Move()
    {
        moving = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moving *= speed;
        moving = transform.rotation * moving;
        playerController.Move(moving * Time.deltaTime);
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 4f, 0));
    }

    private void Axe()
    {
        canChop = false;
        weapoonCollider.enabled = true;
        StartCoroutine(Chop());
    }

    IEnumerator Chop()
    {
        for(int i = 1; i <= 20; i++)
        {
            yield return new WaitForSeconds(0.01f);
            handle.transform.Rotate(0, 0, -3, Space.Self);
        }

        for (int i = 1; i <= 20; i++)
        {
            yield return new WaitForSeconds(0.01f);
            handle.transform.Rotate(0, 0, 3, Space.Self);
        }

        canChop = true;
        weapoonCollider.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("box"))
        {
            message.SetActive(true);
            if (Input.GetKey(KeyCode.Space))
            {
                Deposit();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("box"))
        {
            message.SetActive(false);
        }
    }

    public void Deposit()
    {
        if (woodAmount > 0)
        {
            woodAmount = 0;
            woodMax.SetActive(false);
            StartCoroutine(Dropped());
            coinNumber += 3;
            coins.GetComponent<Text>().text = "" + coinNumber;
        }
        else
        {

        }
    }

    IEnumerator Dropped()
    {
        diposited.SetActive(true);
        yield return new WaitForSeconds(3f);
        diposited.SetActive(false);
    }

    public void Gather()
    {
        if (woodAmount == 0)
        {
            woodAmount += 9;
            woodMax.SetActive(true);
        }
        else
        {
            StartCoroutine(Maxed());
        }
    }

    IEnumerator Maxed()
    {
        maxedWood.SetActive(true);
        yield return new WaitForSeconds(3f);
        maxedWood.SetActive(false);
    }
}
