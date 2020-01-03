using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MatrixController))]
public class AABBCollider : MonoBehaviour {

    private MatrixController MCCache;
    //public Vector3 CubeSize = new Vector3(1,1,1);

    //public VectorAlgorithm.AABB aabb;
    //public VectorAlgorithm.AABB aabb2;

    public GameObject otherBOxGO;
    //GameObject Enemy = GameObject.Find("Enemy");

    //public bool Intersect(AABBCollider other)
    //{
    //    return VectorAlgorithm.AABB.Intersects(aabb, other.aabb);
    //}

    bool Collision()
    {
        float Offset = (GetComponent<MatrixController>().scaleIndex) / 2;

        Vector3 boxMin = VectorAlgorithm.PlayerVector.SubtractVector(GetComponent<MatrixController>().position, new Vector3(Offset, Offset, Offset));
        Vector3 boxMax = VectorAlgorithm.PlayerVector.AddVector(GetComponent<MatrixController>().position, new Vector3(Offset, Offset, Offset));

        Vector3 boxMin2 = VectorAlgorithm.PlayerVector.SubtractVector(otherBOxGO.GetComponent<MatrixController>().position, new Vector3(Offset, Offset, Offset));
        Vector3 boxMax2 = VectorAlgorithm.PlayerVector.AddVector(otherBOxGO.GetComponent<MatrixController>().position, new Vector3(Offset, Offset, Offset));

        VectorAlgorithm.AABB aabb = new VectorAlgorithm.AABB(boxMin, boxMax);
        VectorAlgorithm.AABB aabb2 = new VectorAlgorithm.AABB(boxMin2, boxMax2);

        if (VectorAlgorithm.AABB.Intersects(aabb, aabb2))
        {
           
            Debug.Log("Colliding!");
        }
        else
        {
            Debug.Log("Not Colliding!");
        }
        return false;
    }

    // Use this for initialization
    //void Start () {
    //   MCCache = GetComponent<MatrixController>();
    //   aabb = new VectorAlgorithm.AABB(MCCache.position - CubeSize, MCCache.position + CubeSize);

    //}
	
	// Update is called once per frame
	void Update () {
        Collision();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(GetComponent<MatrixController>().position, new Vector3(1.0f, 1.0f, 1.0f));
    }
}
