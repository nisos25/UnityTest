using UnityEngine;
using Unity.Collections;
using Unity.Jobs;

public class RedTexture : MonoBehaviour
{
    [SerializeField] private Texture2D inputTexture;

    private struct RegionResult
    {
        public int regionIndex;
        public int sum;
    }

    private void Start()
    {
        var results = new NativeArray<RegionResult>(4, Allocator.TempJob);
        var pixelData = new NativeArray<Color>(inputTexture.GetPixels(), Allocator.TempJob);
        
        
        var job = new TextureSumJob()
        {
            pixelData = pixelData,
            inputTextureWidth = inputTexture.width,
            inputTextureHeight = inputTexture.height,
            numRegions = 4,
            results = results
        };
        
        var handle = job.Schedule(4, 1);
        
        handle.Complete();

        var totalSum = 0;
        
        for (int i = 0; i < 4; i++)
        {
            totalSum += results[i].sum;
        }
        
        results.Dispose();
        pixelData.Dispose();
        
        Debug.Log("Total R channel sum: " + totalSum);
    }
    
    private struct TextureSumJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<Color> pixelData;
        public int inputTextureWidth;
        public int inputTextureHeight;
        public int numRegions;
        public NativeArray<RegionResult> results;

        public void Execute(int index)
        {
            var regionWidth = inputTextureWidth / numRegions;
            var startX = regionWidth * index;
            var endX = startX + regionWidth;
            
            var sum = 0;
            
            for (int x = startX; x < endX; x++)
            {
                for (int y = 0; y < inputTextureHeight; y++)
                {
                    int pixelIndex = y * inputTextureWidth + x;
                    Color pixel = pixelData[pixelIndex];
                    sum += (int)(pixel.r * 255);
                }
            }
            
            
            results[index] = new RegionResult()
            {
                regionIndex = index,
                sum = sum
            };
        }
    }
}