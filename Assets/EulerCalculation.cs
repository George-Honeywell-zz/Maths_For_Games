using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EulerCalculation : MonoBehaviour {

    public GameObject Projectile;
    public AudioSource ProjectileSound;

    public static float VectorToRadians(Vector2 V)
    {
        float rv = 0.0f;

        //Atan and Atan2 Functions return a value in RADIANS
        rv = Mathf.Atan2(V.y, V.x);

        return rv;
    }

    public static Vector2 RadiansToVector(float angle)
    {
        Vector2 rv = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        //The above could be written like this to make it easier to understand
        //rv.x = Mathf.Cos(angle);
        //rv.y = Mathf.Sin(angle);

        return rv;
    }

    public static Vector3 EulerAnglesToDirection(Vector3 EulerAngles)
    {
        Vector3 rv = new Vector3();

        EulerAngles = EulerAngles / (180.0f / Mathf.PI);

        //Make Sure the values stored inside 'EulerAngles' are in radians
        //Because Unity Coordinate system is different,
        //X = Left, Right
        //Z = Forward, Back
        rv.z = Mathf.Cos(EulerAngles.y) * Mathf.Cos(EulerAngles.x);
        rv.y = Mathf.Sin(EulerAngles.x);
        rv.x = Mathf.Cos(EulerAngles.x) * Mathf.Sin(EulerAngles.y);

        return rv;
    }

    public static Vector3 VectorCrossProduct(Vector3 A, Vector3 B)
    {
        Vector3 rv = new Vector3();

        rv.x = A.y * B.z - A.z * B.y;
        rv.y = A.z * B.x - A.x * B.z;
        rv.z = A.x * B.y - A.y * B.x;

        return rv;
    }

	// Use this for initialization
	void Start () {
		
	}

    Vector3 eulerRotation = new Vector3();
    Vector3 forwardDirection = new Vector3();
    Vector3 rightDirection = new Vector3();
    public float playerSpeed = 2.0f;

	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
           
            Quaternion rotate = Quaternion.Euler(GetComponent<MatrixController>().rotation);


            GameObject BulletHandler = Instantiate(Projectile,
                                        GetComponent<MatrixController>().position,
                                        rotate) as GameObject;
            ProjectileSound.Play();
        }

        if(Input.GetKey(KeyCode.W))
        {
            GetComponent<MatrixController>().position.x += 1.0f * playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<MatrixController>().position.z += 1.0f * playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<MatrixController>().position.x += -1.0f * playerSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<MatrixController>().position.z += -1.0f * playerSpeed * Time.deltaTime;
        }
    }
}
