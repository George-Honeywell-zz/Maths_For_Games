using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    //public Vector3 Velocity;
    public Vector3 direction;

    // Use this for initialization
    void Start () {

	}

    Vector3 eulerRotation = new Vector3();
    Vector3 forwardDirection = new Vector3();
    Vector3 rightDirection = new Vector3();
    public float enemySpeed = 1.5f;

    // Update is called once per frame
    void Update()
    {
        GameObject PlayerCharacter = GameObject.Find("PlayerCharacter");
        direction = VectorAlgorithm.PlayerVector.SubtractVector(PlayerCharacter.GetComponent<MatrixController>().position, 
                                                                GetComponent<MatrixController>().position);

        //Documentation for getting DIRECTION and DISTANCE from One Object to Another
        //docs.unity3d.com/Manual/DirectionDistanceFromOneObjectToAnother.html
        if (direction.magnitude <= 10)
        {

            direction = VectorAlgorithm.PlayerVector.Normalized(direction);
            Quaternion q = Quaternion.LookRotation(direction);
            Vector3 Euler = q.eulerAngles;
            
            GetComponent<MatrixController>().rotation = q.eulerAngles;
            direction *= Time.deltaTime * enemySpeed;
            
            //transform.position += direction; //Working
            GetComponent<MatrixController>().position += direction;
            
        }
    }
}
