using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MatrixController : MonoBehaviour
{

    public VectorAlgorithm.Matrix4by4 M;
    public Mesh localMesh; // Create localMesh

    Vector3[] ModelSpaceVertices;
    // Use this for initialization
    void Start()
    {
        //Mesh filter is a component the stores information about the current mesh
        MeshFilter MF = GetComponent<MeshFilter>();
        Mesh mesh1 = Mesh.Instantiate(localMesh) as Mesh; //Instantiate mesh1 as localMesh
        MF.sharedMesh = mesh1;
        ModelSpaceVertices = MF.sharedMesh.vertices; //sharedMesh instead of Mesh to share the meshes
    }

    //public float Angle;
    public Vector3 position;
    public Vector3 rotation; //Convert into Radians
    public Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);
    public float scaleIndex = 1.0f;

    // Update is called once per frame
    void Update()
    {
        //Define a new array with the correct size
        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];


        //--- Scaling Matrix (2x, y, z)
        VectorAlgorithm.Matrix4by4 S = new VectorAlgorithm.Matrix4by4(new Vector3(1, 0, 0) * scale.x, 
                                                                      new Vector3(0, 1, 0) * scale.y, 
                                                                      new Vector3(0, 0, 1) * scale.z, 
                                                                      Vector3.zero);

        //--- Transform Matrix ---//
        VectorAlgorithm.Matrix4by4 T = new VectorAlgorithm.Matrix4by4(
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1),
            new Vector3(position.x, position.y, position.z));


        //Create our rotation matrix, this time rotating around the Roll, Pitch and Yaw in that order

        Vector3 rotationRadians = rotation / (180.0f / Mathf.PI);

        VectorAlgorithm.Matrix4by4 rollMatrix = new VectorAlgorithm.Matrix4by4(
            new Vector3(Mathf.Cos(rotationRadians.z), Mathf.Sin(rotationRadians.z), 0),
            new Vector3(-Mathf.Sin(rotationRadians.z), Mathf.Cos(rotationRadians.z), 0),
            new Vector3(0, 0, 1),
            Vector3.zero);

        VectorAlgorithm.Matrix4by4 pitchMatrix = new VectorAlgorithm.Matrix4by4(
            new Vector3(1, 0, 0),
            new Vector3(0, Mathf.Cos(rotationRadians.x), Mathf.Sin(rotationRadians.x)),
            new Vector3(0, -Mathf.Sin(rotationRadians.x), Mathf.Cos(rotationRadians.x)),
            Vector3.zero);

        VectorAlgorithm.Matrix4by4 yawMatrix = new VectorAlgorithm.Matrix4by4(
            new Vector3(Mathf.Cos(rotationRadians.y), 0, -Mathf.Sin(rotationRadians.y)),
            new Vector3(0, 1, 0),
            new Vector3(Mathf.Sin(rotationRadians.y), 0, Mathf.Cos(rotationRadians.y)),
            Vector3.zero);


        VectorAlgorithm.Matrix4by4 R = yawMatrix * (pitchMatrix * rollMatrix);
        M = T * (R * S);


        //Transform each individual vertex
        for (int j = 0; j < TransformedVertices.Length; j++)
        {
            TransformedVertices[j] = M * ModelSpaceVertices[j]; ;
        }

        //Mesh filter is a component the stores information about the current mesh
        MeshFilter MF = GetComponent<MeshFilter>();

        //Assign our new Vertices
        MF.sharedMesh.vertices = TransformedVertices;

        //These final steps are sometimes necessary to make the mesh look correct
        MF.sharedMesh.RecalculateNormals();
        MF.sharedMesh.RecalculateBounds();

    }
}
