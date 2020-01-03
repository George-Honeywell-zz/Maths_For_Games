using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorAlgorithm
{

    public float speed = 5.0f;
    public Vector3 Velocity;

    public class PlayerVector
    {
        public float x, y, z;

        public PlayerVector(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        //ERROR DEBUG
        //'An Object reference is required for the non-static field, method, or property...
        //Ensure the methods are static when calling between them.

        public static Vector3 AddVector(Vector3 Vec1, Vector3 Vec2)
        {
            //Initialise the RETURN VALUE
            Vector3 ReturnValue = new Vector3(0, 0, 0);

            //ADD the vectors together
            ReturnValue.x = Vec1.x + Vec2.x;
            ReturnValue.y = Vec1.y + Vec2.y;
            ReturnValue.z = Vec1.z + Vec2.y;

            return ReturnValue;
        }

        //Subtract Vectors
        public static Vector3 SubtractVector(Vector3 Vec1, Vector3 Vec2)
        {
            Vector3 ReturnValue = new Vector3(0, 0, 0)
            {
                x = Vec1.x - Vec2.x,
                y = Vec1.y - Vec2.y,
                z = Vec1.z - Vec2.z
            };
            return ReturnValue;
        }

        public static float Length(Vector3 A)
        {
            float rv;

            rv = A.x * A.x + A.y * A.y + A.z * A.z;
            rv = Mathf.Sqrt(rv);

            return rv;
        }

        public static float LengthSq(Vector3 A)
        {
            float rv;

            rv = A.x * A.x + A.y * A.y + A.z * A.z;

            return rv;
        }

        public static PlayerVector ScaleVector(PlayerVector Vec1, float Scaler)
        {
            //Initialise our return value
            PlayerVector ReturnValue = new PlayerVector(0, 0);

            //Scale the vector
            ReturnValue.x = Vec1.x * Scaler;
            ReturnValue.y = Vec1.y * Scaler;

            return ReturnValue;
        }

        public static Vector3 DivideVectors(Vector3 Vec1, float Divide)
        {
            //Initialise the return value
            Vector3 ReturnValue = new Vector3(0, 0, 0);

            //Divide the Vectors
            ReturnValue.x = Vec1.x / Divide;
            ReturnValue.y = Vec1.y / Divide;
            ReturnValue.z = Vec1.z / Divide;

            return ReturnValue;
        }

        public static Vector3 Normalized(Vector3 A)
        {
            Vector3 rv = new Vector3(0, 0, 0);
            rv.x = A.x;
            rv.y = A.y;
            rv.z = A.z;

            //Normalize the Vector
            rv = DivideVectors(rv, Length(rv));

            return rv;
        }

        public static float DotProduct(Vector3 A, Vector3 B, bool Normalize = true)
        {
            //If applicable, normalize vectors
            if (Normalize)
            {
                A.Normalize();
                B.Normalize();
            }

            //Then do the Dot Product Formula
            return A.x * B.x + A.y * B.y + A.z * B.z;
        }

        //public static Vector3 DirectionToEuler(Vector3 direction)
        //{

        //}

        public static Vector3 VecLerp(Vector3 A, Vector3 B, float t)
        {
            return A * (1.0f - t) + B * t;
        }

    }

    public class Matrix4by4
    {
        public float[,] values;
        public Matrix4by4(Vector4 column1, Vector4 column2, Vector4 column3, Vector4 column4)
        {
            values = new float[4, 4];

            //Column 1
            values[0, 0] = column1.x;
            values[1, 0] = column1.y;
            values[2, 0] = column1.z;
            values[3, 0] = column1.w;

            //Column 2
            values[0, 1] = column2.x;
            values[1, 1] = column2.y;
            values[2, 1] = column2.z;
            values[3, 1] = column2.w;

            //Column 3
            values[0, 2] = column3.x;
            values[1, 2] = column3.y;
            values[2, 2] = column3.z;
            values[3, 2] = column3.w;

            //Column 4
            values[0, 3] = column4.x;
            values[1, 3] = column4.y;
            values[2, 3] = column4.z;
            values[3, 3] = column4.w;
        }

        public Matrix4by4(Vector3 column1, Vector3 column2, 
                          Vector3 column3, Vector3 column4)
        {
            values = new float[4, 4];

            //Column 1
            values[0, 0] = column1.x;
            values[1, 0] = column1.y;
            values[2, 0] = column1.z;
            values[3, 0] = 0;

            //Column 2
            values[0, 1] = column2.x;
            values[1, 1] = column2.y;
            values[2, 1] = column2.z;
            values[3, 1] = 0;

            //Column 3
            values[0, 2] = column3.x;
            values[1, 2] = column3.y;
            values[2, 2] = column3.z;
            values[3, 2] = 0;

            //Column 4
            values[0, 3] = column4.x;
            values[1, 3] = column4.y;
            values[2, 3] = column4.z;
            values[3, 3] = 1;
        }

        public static Matrix4by4 Identity
        {
            get
            {
                return new Matrix4by4(
                    new Vector4(1, 0, 0, 0),
                    new Vector4(0, 1, 0, 0),
                    new Vector4(0, 0, 1, 0),
                    new Vector4(0, 0, 0, 1));
            }
        }

        public static Vector4 operator *(Matrix4by4 lhs, Vector4 vector)
        {
            Vector4 rv = new Vector4();

            vector.w = 1.0f; //Forcing the W component to 1 for translation Matrix. As Unity is converting a 
                             //Vector4 to a Vector3 implicitly, the W component is set to 0 by default.
                             //With a translation component, the W component must be 1.

            rv.x = lhs.values[0, 0] * vector.x + lhs.values[0, 1] * vector.y + lhs.values[0, 2] * vector.z + lhs.values[0, 3] * vector.w;
            rv.y = lhs.values[1, 0] * vector.x + lhs.values[1, 1] * vector.y + lhs.values[1, 2] * vector.z + lhs.values[1, 3] * vector.w;
            rv.z = lhs.values[2, 0] * vector.x + lhs.values[2, 1] * vector.y + lhs.values[2, 2] * vector.z + lhs.values[2, 3] * vector.w;
            rv.w = lhs.values[3, 0] * vector.x + lhs.values[3, 1] * vector.y + lhs.values[3, 2] * vector.z + lhs.values[3, 3] * vector.w;

            return rv;
        }

        public static Matrix4by4 operator *(Matrix4by4 lhs, Matrix4by4 rhs)
        {
            Matrix4by4 rv = Identity;

            //vector.w = 1.0f; //Forcing the W component to 1 for translation Matrix. 
            //As Unity is converting a Vector4 to a Vector3 implicitly, the W component is set to 0 by default.
            //With a translation component, the W component must be 1.

            rv.values[0, 0] = lhs.values[0, 0] * rhs.values[0, 0] + lhs.values[0, 1] * rhs.values[1, 0] + lhs.values[0, 2] * rhs.values[2, 0] + lhs.values[0, 3] * rhs.values[3, 0];
            rv.values[1, 0] = lhs.values[1, 0] * rhs.values[0, 0] + lhs.values[1, 1] * rhs.values[1, 0] + lhs.values[1, 2] * rhs.values[2, 0] + lhs.values[1, 3] * rhs.values[3, 0];
            rv.values[2, 0] = lhs.values[2, 0] * rhs.values[0, 0] + lhs.values[2, 1] * rhs.values[1, 0] + lhs.values[2, 2] * rhs.values[2, 0] + lhs.values[2, 3] * rhs.values[3, 0];
            rv.values[3, 0] = lhs.values[3, 0] * rhs.values[0, 0] + lhs.values[3, 1] * rhs.values[1, 0] + lhs.values[3, 2] * rhs.values[2, 0] + lhs.values[3, 3] * rhs.values[3, 0];

            rv.values[0, 1] = lhs.values[0, 0] * rhs.values[0, 1] + lhs.values[0, 1] * rhs.values[1, 1] + lhs.values[0, 2] * rhs.values[2, 1] + lhs.values[0, 3] * rhs.values[3, 1];
            rv.values[1, 1] = lhs.values[1, 0] * rhs.values[0, 1] + lhs.values[1, 1] * rhs.values[1, 1] + lhs.values[1, 2] * rhs.values[2, 1] + lhs.values[1, 3] * rhs.values[3, 1];
            rv.values[2, 1] = lhs.values[2, 0] * rhs.values[0, 1] + lhs.values[2, 1] * rhs.values[1, 1] + lhs.values[2, 2] * rhs.values[2, 1] + lhs.values[2, 3] * rhs.values[3, 1];
            rv.values[3, 1] = lhs.values[3, 0] * rhs.values[0, 1] + lhs.values[3, 1] * rhs.values[1, 1] + lhs.values[3, 2] * rhs.values[2, 1] + lhs.values[3, 3] * rhs.values[3, 1];

            rv.values[0, 2] = lhs.values[0, 0] * rhs.values[0, 2] + lhs.values[0, 1] * rhs.values[1, 2] + lhs.values[0, 2] * rhs.values[2, 2] + lhs.values[0, 3] * rhs.values[3, 2];
            rv.values[1, 2] = lhs.values[1, 0] * rhs.values[0, 2] + lhs.values[1, 1] * rhs.values[1, 2] + lhs.values[1, 2] * rhs.values[2, 2] + lhs.values[1, 3] * rhs.values[3, 2];
            rv.values[2, 2] = lhs.values[2, 0] * rhs.values[0, 2] + lhs.values[2, 1] * rhs.values[1, 2] + lhs.values[2, 2] * rhs.values[2, 2] + lhs.values[2, 3] * rhs.values[3, 2];
            rv.values[3, 2] = lhs.values[3, 0] * rhs.values[0, 2] + lhs.values[3, 1] * rhs.values[1, 2] + lhs.values[3, 2] * rhs.values[2, 2] + lhs.values[3, 3] * rhs.values[3, 2];

            rv.values[0, 3] = lhs.values[0, 0] * rhs.values[0, 3] + lhs.values[0, 1] * rhs.values[1, 3] + lhs.values[0, 2] * rhs.values[2, 3] + lhs.values[0, 3] * rhs.values[3, 3];
            rv.values[1, 3] = lhs.values[1, 0] * rhs.values[0, 3] + lhs.values[1, 1] * rhs.values[1, 3] + lhs.values[1, 2] * rhs.values[2, 3] + lhs.values[1, 3] * rhs.values[3, 3];
            rv.values[2, 3] = lhs.values[2, 0] * rhs.values[0, 3] + lhs.values[2, 1] * rhs.values[1, 3] + lhs.values[2, 2] * rhs.values[2, 3] + lhs.values[2, 3] * rhs.values[3, 3];
            rv.values[3, 3] = lhs.values[3, 0] * rhs.values[0, 3] + lhs.values[3, 1] * rhs.values[1, 3] + lhs.values[3, 2] * rhs.values[2, 3] + lhs.values[3, 3] * rhs.values[3, 3];

            return rv;
        }

        public Matrix4by4 TranslationInverse()
        {
            Matrix4by4 rv = Identity;

            rv.values[0, 3] = -values[0, 3];
            rv.values[1, 3] = -values[1, 3];
            rv.values[2, 3] = -values[2, 3];

            return rv;
        }

        public double[,] Transpose(double[,] Matrix4by4)
        {
            int rows = Matrix4by4.GetLength(0);
            int columns = Matrix4by4.GetLength(1);

            double[,] rv = new double[columns, rows];

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    rv[j, i] = Matrix4by4[i, j];
                }
            }
            return rv;
        }

        public Matrix4by4 GetRow(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3)
        {
            Matrix4by4 rv = Identity;

            row0.x = rv.values[0, 0];
            row0.y = rv.values[1, 0];
            row0.z = rv.values[2, 0];
            row0.w = rv.values[3, 0];

            row1.x = rv.values[0, 1];
            row1.y = rv.values[1, 1];
            row1.z = rv.values[2, 1];
            row1.w = rv.values[3, 1];

            row2.x = rv.values[0, 2];
            row2.y = rv.values[1, 2];
            row2.z = rv.values[2, 2];
            row2.w = rv.values[3, 2];

            row3.x = rv.values[0, 3];
            row3.y = rv.values[1, 3];
            row3.z = rv.values[2, 3];
            row3.w = rv.values[3, 3];
            return rv;
        }

        public Vector4 GetRow(int rowIndex)
        {
            return new Vector4(values[0, rowIndex],
                values[1, rowIndex],
                values[2, rowIndex],
                values[3, rowIndex]);
        }

        public Matrix4by4 RotationInverse()
        {
            //Return a new Matrix, by getting the rows in the...
            return new Matrix4by4(GetRow(0), GetRow(1), GetRow(2), GetRow(3));
        }

        public Matrix4by4 ScaleInverse()
        {
            Matrix4by4 rv = Identity;

            rv.values[0, 0] = 1.0f / values[0, 0];
            rv.values[1, 1] = 1.0f / values[1, 1];
            rv.values[2, 2] = 1.0f / values[2, 2];

            return rv;
        }

        public static Vector3 RotateVertexAroundAxis(float Angle, Vector3 Axis, Vector3 Vertex)
        {
            //Rodrigues Rotation Formula
            //Efficient way of rotating a vector in space, given an axis angle of rotation. 
            Vector3 rv = (Vertex * Mathf.Cos(Angle)) +
                PlayerVector.DotProduct(Vertex, Axis) * Axis * (1 - Mathf.Cos(Angle)) +
                EulerCalculation.VectorCrossProduct(Axis, Vertex) * Mathf.Sin(Angle);

            return rv;
        }

    }

    public class Quat
    {
        public float w, x, y, z;

        public Quat(float Angle, Vector3 Axis)
        {

            /*
             * Define a constructor that takes in and calculate the Axis Angle of the Quatenion
             */

            float halfAngle = Angle / 2;
            w = Mathf.Cos(halfAngle);
            x = Axis.x * Mathf.Sin(halfAngle);
            y = Axis.y * Mathf.Sin(halfAngle);
            z = Axis.z * Mathf.Sin(halfAngle);
        }

        public static Quat operator *(Quat lhs, Quat rhs)
        {
            Quat rv = new Quat(0, new Vector3(0, 0, 0));
            
            /*
             * To Multiply two Quaternions, the following formula must take place;
             * --- Calculate the W Component of the new Quaternion ---
             * FIRST: Times the W Component of both Quaternions together
             * SECOND: Get the DotProduct between the two Vector Components of both Quaternions
             * THIRD: Minus both results from each other.
             * This returns the W Component of the new Quaternion.
             * 
             * --- Calculate the Vector Component of the Quaternion ---
             * FIRST: Multiply the W component of RHS, by the Vector component of LHS quaternion
             * SECOND: Multiply the W component of LHS, by the Vector component of RHS quaternion
             * THIRD: Calculate the Cross Product of the Quaternions Vector Component
             * FOURTH: Add these results together, and store them in a new Variable.
             * FIFTH: Set the Axis of the new quaternion
            */

            rv.w = (rhs.w * lhs.w) - VectorAlgorithm.PlayerVector.DotProduct(rhs.GetAxis(), lhs.GetAxis());
            Vector3 QuatVector = rhs.w * lhs.GetAxis() + lhs.w * rhs.GetAxis() + EulerCalculation.VectorCrossProduct(lhs.GetAxis(), rhs.GetAxis());

            rv.SetAxis(QuatVector);

            return rv;
        }

        public static Vector3 GetAxis(Quat A)
        {
            //Get the Vector section of the Quaternion
            return new Vector3(A.x, A.y, A.z);
        }

        public Vector3 GetAxis()
        {
            //Returns a new Vector3 to a quaternion
            return new Vector3(x, y, z);
        }

        public void SetAxis(Vector3 A)
        {
            //Set the Vector3 component in a Quaternion
            x = A.x;
            y = A.y;
            z = A.z;
        }

        public Quat Inverse()
        {
            Quat rv = new Quat(0, new Vector3(0,0,0));
            //Invserses the Quaternion to complete the rest of the rotation
            //As Quaternions rotate in 4D, this brings the rotation back to 3D (XYZ)
            rv.w = w;

            rv.SetAxis(-GetAxis());

            return rv;
        }

    }
    public class AABB
    {
        public Vector3 BoxCentre; //Vector3 Holding Centre of the Box
        public Vector3 BoxHalf; //Vector3 holding the half extent of the box

        public AABB(Vector3 Min, Vector3 Max)
        {
            MinExtent = Min;
            MaxExtent = Max;

            BoxHalf = (MaxExtent - MinExtent) * 0.5f;
        }

        //This could be a Vector2 if you want to do 2D Bounding Boxes
        Vector3 MinExtent;
        Vector3 MaxExtent;

        public AABB()
        {
            //Set the Extents
            MinExtent = new Vector3(-1, -1, -1);
            MaxExtent = new Vector3(1, 1, 1);

            //Create half of the cubes extent
            BoxHalf = (MaxExtent - MinExtent) * 0.5f;

            //Create the box centre
            BoxCentre = BoxHalf - MinExtent;
        }

        public float Top
        {
            get { return MaxExtent.y; }
        }

        public float Bottom
        {
            get { return MinExtent.y; }
        }

        public float Left
        {
            get { return MinExtent.x; }
        }

        public float Right
        {
            get { return MaxExtent.x; }
        }

        public float Front
        {
            get { return MaxExtent.z; }
        }

        public float Back
        {
            get { return MinExtent.z; }
        }

        public static bool Intersects(AABB Box1, AABB Box2)
        {
            return !(Box2.Left > Box1.Right
                || Box2.Right < Box1.Left
                || Box2.Top < Box1.Bottom
                || Box2.Bottom > Box1.Top
                || Box2.Back > Box1.Front
                || Box2.Front < Box1.Back);    
        }

        //public static bool Collide(AABB Box1, AABB Box2)
        //{

        //    //Formula taken from Christer Ericson Collision Detection Page 79
        //    Box1.BoxHalf = (Box1.MaxExtent - Box1.MinExtent) * 0.5f;
        //    Box2.BoxHalf = (Box2.MaxExtent - Box2.MinExtent) * 0.5f;

        //    Box1.BoxCentre = Box1.BoxHalf + Box1.MinExtent;
        //    Box2.BoxCentre = Box2.BoxHalf + Box2.MinExtent;

        //    if (Mathf.Abs(Box1.BoxCentre.x - Box2.BoxCentre.x) > (Box1.BoxHalf.x + Box2.BoxHalf.x))
        //        return false;

        //    if (Mathf.Abs(Box1.BoxCentre.y - Box2.BoxCentre.y) > (Box1.BoxHalf.y + Box2.BoxHalf.y))
        //        return false;

        //    if (Mathf.Abs(Box1.BoxCentre.z - Box2.BoxCentre.z) > (Box1.BoxHalf.z + Box2.BoxHalf.z))
        //        return false;

        //    return true;

        //}
            

    }
}


