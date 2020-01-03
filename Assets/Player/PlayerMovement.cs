using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 5.0f;
    public Vector3 Velocity;


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        GameObject PlayerCharacter = GameObject.Find("PlayerCharacter");
        //GameObject Enemy = GameObject.Find("Enemy");

        RaycastHit mouse;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out mouse))
        {
            Vector3 direction = mouse.point - PlayerCharacter.GetComponent<MatrixController>().position;
            direction.y = 0;
            //Convert Direction to Euler angle
            Quaternion q = Quaternion.LookRotation(direction);
           // Vector3 Euler = q.eulerAngles;

            //Convert QUATERNION to EULER

            GetComponent<MatrixController>().rotation = q.eulerAngles;
            //Debug.Log("Has Collided");
        }
    }
}
