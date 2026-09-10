using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class S04_CustomCubeMesh : MonoBehaviour
{
    void Start()
    {
        // 지난 시간(S3)에 정의한 정육면체 8개 정점 그대로 재사용
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0f, 0f, 0f),     // 0 - 밑면
            new Vector3(1f, 0f, 0f),     // 1 - 밑면
            new Vector3(1f, 0f, 1f),     // 2 - 밑면
            new Vector3(0f, 0f, 1f),     // 3 - 밑면
            new Vector3(0.5f, 1f, 0.5f), // 4 - 꼭대기 (밑면 중심 바로 위)
            /*
            new Vector3(0f, 0f, 0f), // 0
            new Vector3(1f, 0f, 0f), // 1
            new Vector3(1f, 1f, 0f), // 2
            new Vector3(0f, 1f, 0f), // 3
            new Vector3(0f, 0f, 1f), // 4
            new Vector3(1f, 0f, 1f), // 5
            new Vector3(1f, 1f, 1f), // 6
            new Vector3(0f, 1f, 1f), // 7
            */
        };

        // TODO: 6개 면을 각각 삼각형 2개씩 채우세요.
        // 힌트: z=0 면(0,1,2,3) / z=1 면(4,5,6,7) / y=0 면(0,1,5,4)
        //       y=1 면(3,2,6,7) / x=0 면(0,3,7,4) / x=1 면(1,2,6,5)
        int[] triangles = new int[]
        {
            0, 1, 2,   // 밑면
            0, 2, 3,   // 밑면
            0, 4, 1,   // 옆면
            1, 4, 2,   // 옆면
            2, 4, 3,   // 옆면
            3, 4, 0,   // 옆면

            /* 비뚤어진 Pyramid
            0,2,1,
            0,4,2,
            0,1,4,
            1,4,2,
            */
            
            // z=0 면 (0,1,2,3)

            // z=1 면 (4,5,6,7)

            // y=0 면 (0,1,5,4)

            // y=1 면 (3,2,6,7)

            // x=0 면 (0,3,7,4)

            // x=1 면 (1,2,6,5)
        };

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
    }
}
