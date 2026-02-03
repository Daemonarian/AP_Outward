using UnityEngine;

public class RgbShaderEffect : MonoBehaviour
{
    public float Speed = 0.5f;   // How fast the colors cycle
    public float Intensity = 2f; // How bright the glow is (HDR)

    private MeshRenderer _renderer;
    private int _emissionID;

    void Awake()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();
        // Cache the shader property ID for performance
        _emissionID = Shader.PropertyToID("_Color");
    }

    void Update()
    {
        if (_renderer == null) return;

        // 1. Calculate the color based on Time
        // Mathf.Repeat loops the value between 0.0 and 1.0
        var hue = Mathf.Repeat(Time.time * Speed, 1.0f);

        // 2. Convert Hue to RGB (Saturation 1, Value 1)
        var rainbowColor = Color.HSVToRGB(hue, 1.0f, 1.0f);

        // 3. Apply to the material (Instanced)
        // Multiply by Intensity for that HDR "Bloom" look
        _renderer.material.SetColor(_emissionID, rainbowColor * Intensity);
    }
}