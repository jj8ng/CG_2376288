using UnityEngine;

// 실습: 기울이기(shear) 변환 행렬을 적용해볼 것
// (x, y, z) → (x + k·y, y, z)
[ExecuteAlways]
[RequireComponent(typeof(DiamondMesh))]
public class S09_Shear : MonoBehaviour
{
    [SerializeField] float k = 1.8f;   // (학번 끝자리 = 8 + 1) ÷ 5

    DiamondMesh diamondMesh;

    void OnEnable()
    {
        diamondMesh = GetComponent<DiamondMesh>();
    }

    void Start()
    {
        // 꼭대기 정점 결과 확인용 로그
        Vector3 apex = new Vector3(0.5f, 1f, 0.5f);
        Vector3 sheared = FromHomogeneous(MultiplyMatrixVectorRaw(ShearMatrixRaw(k), ToHomogeneous(apex)));
        Debug.Log($"k={k} → apex {apex} 이동 후 {sheared}");
    }

    void Update()
    {
        if (diamondMesh == null || diamondMesh.BaseVertices == null) return;

        float[,] S = ShearMatrixRaw(k);
        Vector3[] baseVertices = diamondMesh.BaseVertices;
        Vector3[] verts = new Vector3[baseVertices.Length];
        for (int i = 0; i < baseVertices.Length; i++)
            verts[i] = FromHomogeneous(MultiplyMatrixVectorRaw(S, ToHomogeneous(baseVertices[i])));
        diamondMesh.SetVertices(verts);
    }

    // 기울이기 행렬 (e₁, e₃, 원점은 그대로 / e₂ → (k, 1, 0))
    float[,] ShearMatrixRaw(float kValue)
    {
        return new float[,] {
            { 1f, kValue, 0f, 0f },
            { 0f, 1f, 0f, 0f },
            { 0f, 0f, 1f, 0f },
            { 0f, 0f, 0f, 1f }
        };
    }

    Vector4 ToHomogeneous(Vector3 v)
    {
        return new Vector4(v.x, v.y, v.z, 1f);
    }

    Vector3 FromHomogeneous(Vector4 h)
    {
        return new Vector3(h.x, h.y, h.z);
    }

    Vector4 MultiplyMatrixVectorRaw(float[,] M, Vector4 v)
    {
        float[] input = { v.x, v.y, v.z, v.w };
        float[] result = new float[4];
        for (int row = 0; row < 4; row++)
            for (int col = 0; col < 4; col++)
                result[row] += M[row, col] * input[col];
        return new Vector4(result[0], result[1], result[2], result[3]);
    }
}