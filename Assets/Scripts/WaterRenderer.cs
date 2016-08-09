using UnityEngine;
using System.Collections;

public class WaterRenderer : MonoBehaviour
{
    public Color m_colorTop;
    public Color m_colorBottom;
 
    private int m_heightsUniformId;
    private MaterialPropertyBlock m_materialPropertyBlock;
    private Material m_material;
    private Mesh m_mesh;

    private float[] m_heights;

    public void Init(float[] heights)
    {
        m_heights = heights;
    }

    void Awake()
    {
        InitMesh();
        InitMaterial();

        m_heightsUniformId = Shader.PropertyToID("_Heights");

        m_materialPropertyBlock = new MaterialPropertyBlock();
    }

    private void InitMaterial()
    {
        m_material = new Material(Shader.Find("Unlit/Water"));
        m_material.SetColor("_ColorTop", m_colorTop);
        m_material.SetColor("_ColorBottom", m_colorBottom);
    }

    private void InitMesh()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-1, -1),
            new Vector3( 1, -1),
            new Vector3( 1,  1),
            new Vector3(-1,  1)
        };

        int[] triangles = new int[]
        {
            2, 1, 0,
            2, 0, 3
        };

        m_mesh = new Mesh();
        m_mesh.vertices = vertices;
        m_mesh.triangles = triangles;
    }

    private void Update()
    {
        if (m_heights == null)
            return;

        m_materialPropertyBlock.SetFloatArray(m_heightsUniformId, m_heights);
        Graphics.DrawMesh(m_mesh, transform.localToWorldMatrix, m_material, 0, null, 0, m_materialPropertyBlock);
    }
}
