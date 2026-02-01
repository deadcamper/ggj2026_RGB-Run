using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class PanMaterial : MonoBehaviour
{
    public float speed = .01f;
    MeshRenderer MeshRenderer => _meshRenderer ?? (_meshRenderer = GetComponent<MeshRenderer>());
    MeshRenderer _meshRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeshRenderer.material.mainTextureOffset = new Vector2(Random.value, Random.value) * 10;
    }

    // Update is called once per frame
    void Update()
    {
        MeshRenderer.material.mainTextureOffset += new Vector2(Time.deltaTime, Time.deltaTime) * speed;
    }
}
