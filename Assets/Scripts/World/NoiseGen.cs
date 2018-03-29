using Misc;
using UnityEngine;

namespace World {
    public static class NoiseGen {
        public static float[,] Generate2DHeightMap(int chunkSize, int seed, NoiseMethodType type, int octaves,
            float persistance, float lacunarity, float strength, Vector3 offset) {
            float[,] noiseMap = new float[chunkSize,chunkSize];

            offset.x += seed;
            offset.y += seed;
            
            int dimensions = 2;
            int frequency = 1;
            
            Vector3 point00 = new Vector3(-0.5f,-0.5f) + offset;
            Vector3 point10 = new Vector3( 0.5f,-0.5f) + offset;
            Vector3 point01 = new Vector3(-0.5f, 0.5f) + offset;
            Vector3 point11 = new Vector3( 0.5f, 0.5f) + offset;

            NoiseMethod method = Noise.methods[(int)type][dimensions - 1];
            float stepSize = 1f / chunkSize;
            for (int y = 0; y < chunkSize; y++) {
                Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
                Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
                for (int x = 0; x < chunkSize; x++) {
                    Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
                    float sample = Noise.Sum(method, point, frequency, octaves, lacunarity, persistance).value;
                    float amplitude =  strength / frequency;
                    sample += 0.5f;
                    sample *= amplitude;
                    noiseMap[x, y] = sample;
                }
            }      
            return noiseMap;
        }

        public static float[,,] Generate3DHeightMap(int chunkSize, int seed, NoiseMethodType type, int octaves,
            float persistance, float lacunarity, float strength, Vector3 offset) {
            float[,,] noiseMap = new float[chunkSize, chunkSize, chunkSize];
            
            int dimensions = 3;
            int frequency = 1;

            
            Vector3 point00 = new Vector3(-0.5f,-0.5f) + offset;
            Vector3 point10 = new Vector3( 0.5f,-0.5f) + offset;
            Vector3 point01 = new Vector3(-0.5f, 0.5f) + offset;
            Vector3 point11 = new Vector3( 0.5f, 0.5f) + offset;
            
            NoiseMethod method = Noise.methods[(int)type][dimensions - 1];
            float stepSize = 1f / chunkSize;
            for (int y = 0; y < chunkSize; y++) {
                Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
                Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
                for (int x = 0; x < chunkSize; x++) {
                    for (int z = 0; z < chunkSize; z++) {
                        Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
                        float sample = Noise.Sum(method, point, frequency, octaves, lacunarity, persistance).value;
                        float amplitude =  strength / frequency;
                        sample += 0.5f;
                        sample *= amplitude;
                        noiseMap[x, y, z] = sample;
                    }

                    
                }
            }   
            
            
            return noiseMap;
        }
        



    }
}