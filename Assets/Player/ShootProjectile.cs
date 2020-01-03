using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour {


    public GameObject Bullet; //Bullet Emitter
    public GameObject Projectile; // Bullet
    public float ProjectileForce;
    public AudioSource sound;

    public GameObject EnemyPrefab;
    public GameObject[] Enemy;

	// Use this for initialization
	void Start () {
		if(Enemy == null)
        {
            Enemy = GameObject.FindGameObjectsWithTag("EnemyTag");
        }

        Destroy(gameObject, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<MatrixController>().position += 
        EulerCalculation.EulerAnglesToDirection(GetComponent<MatrixController>().rotation) 
        * ProjectileForce 
        * Time.deltaTime;

    }
}
