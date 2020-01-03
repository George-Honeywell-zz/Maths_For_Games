using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int startHealth = 10;
    public GameObject hitParticles;

    private int currentHealth;

	// Use this for initialization
	void Start () {
        currentHealth = startHealth;
	}

    public void TakeDamage(int damage, Vector3 hitPoint)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
