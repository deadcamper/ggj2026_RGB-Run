public static class NullableUtility
{
    public static bool TryGetValue<T>(this T? nullable,  out T value)
        where T : struct
    {
        value = nullable ?? default;
        return nullable.HasValue;
    }
}



/*
[RequireComponent(typeof(Camera))]
public class RGBChannelCamera : MonoBehaviour
{
    public Camera Camera => (_camera != null) ? _camera : (_camera = GetComponent<Camera>());
    private Camera _camera;

    [SerializeField] private Shader shader;

    public Material BlitMaterial => _blit_material;
    private Material _blit_material;

    public RenderTexture RenderTexture => renderTexture;
    private RenderTexture renderTexture;



    public ColorChannel ColorChannel => _ColorChannel;
    [SerializeField] private ColorChannel _ColorChannel;

    protected void Awake()
    {
        _blit_material = CoreUtils.CreateEngineMaterial(shader);
        SetupRenderTexture();
    }


    protected void LateUpdate()
    {
        if (renderTexture == null || renderTexture.width != Screen.width || renderTexture.height != Screen.height)
        {
            SetupRenderTexture();
        }
    }

    private void SetupRenderTexture()
    {
        DestroyRenderTexture();

        int width = Screen.width;
        int height = Screen.height;
        // int width = cam.pixelWidth;
        // int height = cam.pixelHeight;

        renderTexture = new RenderTexture(width, height, 24); // 24 is the depth buffer size (can be 0, 16, or 24)
        renderTexture.Create();

        Camera.targetTexture = renderTexture;

        Debug.Log($"Created new RenderTexture with resolution: {width}x{height}");
    }

    private void DestroyRenderTexture()
    {
        // Release the existing render texture if it exists to free memory
        if (renderTexture != null)
        {
            renderTexture.Release();
            Destroy(renderTexture); // Use Destroy when dynamically creating assets
        }
    }

    private void OnDestroy()
    {
        Destroy(_blit_material);
        DestroyRenderTexture();
    }
}


public class RGBCameraFilter : MonoBehaviour
{
    public Camera Camera => (_camera != null) ? _camera : (_camera = GetComponent<Camera>());
    private Camera _camera;

    public ColorChannels ColorChannels 
    {
        get => colorChannels;
        set => colorChannels = value;
    }
    [SerializeField] private ColorChannels colorChannels = ColorChannels.Red | ColorChannels.Green | ColorChannels.Blue;


    protected void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        src.
        for (int x = 0; x < src.width; x++)
        {
            for (int y = 0; y < src.height; y++)
            {
                src.rea
            }
        }


        foreach(channelCameras)
        {

        }

        if (effectMaterial != null)
        {
            // Apply the shader effect using Graphics.Blit
            // src is the source texture (what the camera rendered)
            // dest is the destination (the screen or a new render texture)
            Graphics.Blit(src, dest, effectMaterial);
        }
        else
        {
            // If no material is assigned, just copy the source directly to the destination
            Graphics.Blit(src, dest);
        }
    }
}

public class FilterRGBRendererFeature : ScriptableRendererFeature
{

    public class Pass : ScriptableRenderPass
    {
        private class Data
        {
            Material material;
        }

        FilterRGBRendererFeature feature;
        RTHandle cameraColorTarget;

        public Pass(FilterRGBRendererFeature feature)
        {
            this.feature = feature;
            renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        }

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameContext)
        {
            foreach (var material in feature.materials)
            {
                if (material == null) return;
            }

            // Get URP camera data
            UniversalResourceData resourceData = frameContext.Get<UniversalResourceData>();

            // Define the pass
            using (var builder = renderGraph.AddRasterRenderPass<Data>("My Custom Pass", out var passData))
            {
                // Set up input and output textures
                builder.UseTexture(resourceData.activeColorTexture, AccessFlags.ReadWrite);

                passData.material = m_Material;

                // Define the execution lambda
                builder.SetRenderFunc((PassData data, RasterGraphContext context) =>
                    ExecutePass(data, context));
            }
        }

        private static void ExecutePass(PassData data, RasterGraphContext context)
        {
            // Perform the blit or command
            Blitter.BlitTexture(context.cmd, new Vector2(1, 1), data.material, 0);
        }
    }

    [SerializeField] private Shader shader;

    public ColorChannels channels;

    private Pass renderPass = null;

    private readonly Dictionary<ColorChannels, Material> existingMaterials = new();

    public Material GetMaterial(ColorChannels colorChannels)
    {
        if (!existingMaterials.TryGetValue(colorChannels, out var material))
        {
            material = CoreUtils.CreateEngineMaterial(shader);
            material.SetColor("_multColor", colorChannels.ToColor());
            existingMaterials[colorChannels] = material;
        }
        return material;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer,
                                    ref RenderingData renderingData)
    {
        if (renderingData.cameraData.cameraType == CameraType.Game
            && renderingData.cameraData.camera == Camera.main)
        {
            renderPass.ConfigureInput(ScriptableRenderPassInput.None);
            renderer.EnqueuePass(renderPass);
        }
    }

    public override void Create()
    {
        renderPass = new Pass(this);
    }

    protected override void Dispose(bool disposing)
    {
        foreach (var material in existingMaterials.Values)
        {
            if (material)
            {
                CoreUtils.Destroy(material);
            }
        }
    }
}*/