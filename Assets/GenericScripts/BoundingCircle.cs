using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingCircle : MonoBehaviour {

    public class BoundingCircles
    {
        public Vector3 CentrePoint;
        public float Radius;

        public BoundingCircles(Vector3 CentrePoint, float Radius)
        {
            this.CentrePoint = CentrePoint;
            this.Radius = Radius;
        }

        public bool Intersects(BoundingCircles otherCircle)
        {
            //Create a vector representing the direction and length of the other circle
            Vector3 VectorToOther = otherCircle.CentrePoint - CentrePoint;

            //Calculate our combined radii squared
            float CombinedRadiusSq = (otherCircle.Radius + Radius);
            CombinedRadiusSq *= CombinedRadiusSq;

            //Return the boolean statement below, if true they intersect
            return VectorAlgorithm.PlayerVector.LengthSq(VectorToOther) <= CombinedRadiusSq;
        }	
	}

    public class Player : MonoBehaviour
    {
        BoundingCircles boundingCircle;
        void Start()
        {
            boundingCircle = new BoundingCircles(GetComponent<MatrixController>().position, 5.0f);
        }

        void Update()
        {
            boundingCircle.CentrePoint = GetComponent<MatrixController>().position;

            //List<Enemy> EnemyList = GetEnemyList();

            //foreach(Enemy e in EnemyList)
            //{
            //    if(boundingCircle.Intersects(e.BoundingCircle))
            //    {
            //        Debug.Log("Colliding!");
            //    }
            //    else
            //    {
            //        Debug.Log("Not Colliding...");
            //    }
            //}
        }
    }
}
