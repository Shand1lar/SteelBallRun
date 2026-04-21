using UnityEngine;

public class WaterScroll : MonoBehaviour
{
    public float scrollX = 0.02f;
    public float scrollY = 0.02f;

    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float offsetX = Time.time * scrollX;
        float offsetY = Time.time * scrollY;

        mat.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}