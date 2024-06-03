using UnityEngine;

public class QuadRenderer
{
    public GameObject gO;
    public string name;
    public float width;
    public float height;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private Mesh mesh;

    public void SetSprite(Sprite sprite)
    {
        meshRenderer.sharedMaterial.SetTexture("_MainTex", sprite.texture);
    }

    public void Create(string name, Transform parent)
    {
        gO = new GameObject();
        gO.name = name;
        gO.transform.parent = parent;
        gO.transform.localPosition = new Vector3(0, 0, 0);

        meshFilter = gO.AddComponent<MeshFilter>();
        meshRenderer = gO.AddComponent<MeshRenderer>();
        mesh = new Mesh();
        mesh.name = "CustomQuad";
        meshFilter.mesh = mesh;
    }

    public void ReGenerate(float widthToSetTo, float heightToSetTo, string uniqueMaterialShaderName)
    {
        Shader uniqueMaterialShader = Shader.Find(uniqueMaterialShaderName);
        if (uniqueMaterialShaderName == null)
        {
            Debug.LogError("Shader not found: ''" + uniqueMaterialShaderName + "''!");
            return;
        }
        SetVertices(widthToSetTo, heightToSetTo);
        SetTriangles();
        SetNormals();
        SetUVs();
        FlipNormals();
        SetUniqueMaterial(uniqueMaterialShader);
    }

    private void SetUniqueMaterial(Shader materialShader)
    {
        Material uniqueMaterial = new Material(materialShader);
        meshRenderer.sharedMaterial = uniqueMaterial;
    }

    private void SetUVs()
    {
        Vector2[] uvs = new Vector2[4];
        uvs[0] = new Vector2(0.0f, 0.0f);
        uvs[1] = new Vector2(1f, 0.0f);
        uvs[2] = new Vector2(0.0f, 1f);
        uvs[3] = new Vector2(1f, 1f);
        mesh.uv = uvs;
    }

    private void SetVertices(float widthToSetTo, float heightToSetTo)
    {
        Vector3[] vertices = new Vector3[4];

        float offset = 0.5f;

        vertices[0] = new Vector3(0 - offset, 0, 0);
        vertices[1] = new Vector3(widthToSetTo - offset, 0, 0);
        vertices[2] = new Vector3(0 - offset, heightToSetTo, 0);
        vertices[3] = new Vector3(widthToSetTo - offset, heightToSetTo, 0);
        mesh.vertices = vertices;
    }

    private void SetTriangles()
    {
        int[] tri = new int[6];

        //  Lower left triangle.
        tri[0] = 0;
        tri[1] = 2;
        tri[2] = 1;

        //  Upper right triangle.
        tri[3] = 2;
        tri[4] = 3;
        tri[5] = 1;

        mesh.triangles = tri;
    }

    private void SetNormals()
    {
        Vector3[] normals = new Vector3[4];

        normals[0] = -Vector3.forward;
        normals[1] = -Vector3.forward;
        normals[2] = -Vector3.forward;
        normals[3] = -Vector3.forward;

        mesh.normals = normals;
    }

    private void FlipNormals()
    {
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
            normals[i] = -normals[i];
        mesh.normals = normals;

        for (int m = 0; m < mesh.subMeshCount; m++)
        {
            int[] triangles = mesh.GetTriangles(m);
            for (int i = 0; i < triangles.Length; i += 3)
            {
                int temp = triangles[i + 0];
                triangles[i + 0] = triangles[i + 1];
                triangles[i + 1] = temp;
            }
            mesh.SetTriangles(triangles, m);
        }
    }

}
