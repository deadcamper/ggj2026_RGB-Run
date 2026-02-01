using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PerRGBChannel<T> : IEnumerable<T>
{
    public struct Enumerator : IEnumerator<T>
    {
        private int index;
        private PerRGBChannel<T> data;
        public T Current 
            => index switch
            {
                0 => throw new System.Exception("Not started"),
                1 => throw new System.Exception("Not started"),
                2 => throw new System.Exception("Not started"),
                3 => throw new System.Exception("Not started"),
                _ => throw new System.IndexOutOfRangeException(),
            };


        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            index++;
            return index < 4;
        }

        public void Reset()
        {
            index = 0;
        }
    }

    public T R => r;
    [SerializeField] private T r;
    public T G => g;
    [SerializeField] private T g;
    public T B => b;
    [SerializeField] private T b;
    public T this[ColorChannel channel]
        => channel switch
        {
            ColorChannel.Red => r,
            ColorChannel.Green => g,
            ColorChannel.Blue => b,
            _ => throw new System.ArgumentOutOfRangeException(),
        };
    public T this[int channel]
        => channel switch
        {
            0 => r,
            1 => g,
            2 => b,
            _ => throw new System.IndexOutOfRangeException(),
        };


    public PerRGBChannel(T r, T g, T b) => (this.r, this.g, this.b) = (r, g, b);

    public PerRGBChannel<T> Shuffled()
        => UnityEngine.Random.Range(0, 6) switch
        {
            0 => new(R, G, B),
            1 => new(R, B, G),
            2 => new(B, R, G),
            3 => new(B, G, R),
            4 => new(G, R, B),
            5 => new(G, B, R),
            _ => throw new System.IndexOutOfRangeException(),
        };

    public Enumerator GetEnumerator()
        => new Enumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
        => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}



/*
public class RGBBlitRendererFeature : ScriptableRendererFeature
{

    public class Pass : ScriptableRenderPass
    {
        private class Data
        {
            PerRGBChannel<Material> materials;
        }

        RGBBlitRendererFeature feature;
        RTHandle cameraColorTarget;

        public Pass(RGBBlitRendererFeature feature)
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

    [SerializeField] private PerRGBChannel<Camera> cameras;

    public ColorChannels channels;

    private PerRGBChannel<Material> materials;

    private PerRGBChannel<RenderTexture> renderTextures;

    private Pass renderPass = null;

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
        materials = new PerRGBChannel<Material>(
            CreateMaterial(ColorChannel.Red),
            CreateMaterial(ColorChannel.Green),
            CreateMaterial(ColorChannel.Blue));


        renderPass = new Pass(this);
    }

    private Material CreateMaterial(ColorChannel channel)
    {
        var material = CoreUtils.CreateEngineMaterial(shader);

        return material;
    }

    protected override void Dispose(bool disposing)
    {
        foreach (var material in materials)
        {
            if (material)
            {
                CoreUtils.Destroy(material);
            }
        }
    }
}
*/