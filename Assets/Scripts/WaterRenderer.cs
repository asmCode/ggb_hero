using UnityEngine;
using System.Collections;

public class WaterRenderer : MonoBehaviour
{
    public bool m_transparent = false;
    public Color m_colorTop;
    public Color m_colorBottom;
 
    private int m_heightsUniformId;
    private MaterialPropertyBlock m_materialPropertyBlock;
    private Material m_material;
    private Mesh m_mesh;
    private MeshRenderer m_meshRenderer;

    private float[] m_heights;

    public void Init(float[] heights)
    {
        m_heights = heights;
    }

    void Awake()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
        if (m_transparent)
            GetComponent<MeshRenderer>().sortingLayerID = SortingLayer.NameToID("PixelWater");

        InitMesh();
        InitMaterial();

        m_heightsUniformId = Shader.PropertyToID("_Heights");

        m_materialPropertyBlock = new MaterialPropertyBlock();
    }

    private void InitMaterial()
    {
        string shaderName = m_transparent ? "Unlit/WaterTransparent" : "Unlit/Water";

        m_material = new Material(Shader.Find(shaderName));
        m_material.SetColor("_ColorTop", m_colorTop);
        m_material.SetColor("_ColorBottom", m_colorBottom);

        m_meshRenderer.material = m_material;
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

        GetComponent<MeshFilter>().mesh = m_mesh;

    }

    private void Update()
    {
        if (m_heights == null)
            return;

        m_materialPropertyBlock.SetFloatArray(m_heightsUniformId, m_heights);
        m_meshRenderer.SetPropertyBlock(m_materialPropertyBlock);

        // Graphics.DrawMesh(m_mesh, transform.localToWorldMatrix, m_material, 0, null, 0, m_materialPropertyBlock);
    }
}
