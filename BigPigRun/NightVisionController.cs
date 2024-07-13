using UnityEngine;

public class NightVisionController : MonoBehaviour
{
    public Shader nightVisionShader;

    private Camera nightVisionCamera;
    private Material nightVisionMaterial;

    void Start()
    {
        nightVisionCamera = GetComponent<Camera>();
        nightVisionMaterial = new Material(nightVisionShader);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, nightVisionMaterial);
    }
}