using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PanMaterial : MonoBehaviour
{
    public float speed = .01f;
    Renderer Renderer => _renderer ?? (_renderer = GetComponent<Renderer>());
    Renderer _renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Renderer.material.mainTextureOffset = new Vector2(Random.value, Random.value) * 10;
    }

    // Update is called once per frame
    void Update()
    {
        Renderer.material.mainTextureOffset += new Vector2(Time.deltaTime, Time.deltaTime) * speed;
    }
}
