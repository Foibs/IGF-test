using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public GameObject gathered;

    private void Start()
    {
        currentHealth = 5;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("axe"))
        {
            currentHealth -= 1;

            if (currentHealth <= 0)
            {
                Lumber();
            }
        }
    }

    private void Lumber()
    {
        gathered.GetComponent<Movement>().Gather();
        Destroy(gameObject);
    }
}
