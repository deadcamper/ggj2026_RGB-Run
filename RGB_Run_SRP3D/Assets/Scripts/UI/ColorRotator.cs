using UnityEngine;
using UnityEngine.UI;

public class ColorRotator : MonoBehaviour
{
    public Image image;

    public Color startingColor = Color.red;

    public float speed = 30f;

    private float saturation;
    private float value;
    private float hue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Color.RGBToHSV(startingColor, out hue, out value, out saturation);
    }

    // Update is called once per frame
    void Update()
    {
        hue = hue + (Time.deltaTime * speed / 360);
        hue %= 360;

        image.color = Color.HSVToRGB(hue, saturation, value);
    }
}
