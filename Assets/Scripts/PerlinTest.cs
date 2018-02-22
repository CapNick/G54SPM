using UnityEngine;

public class PerlinTest : MonoBehaviour{
    [Space, Header("Terrain Generation")]
    public int Seed;
    public int MapWidth;
    public int MapHeight;
    public float Scale;
    public int Octaves;
    public float Persistance;
    public float Lacunarity;
    public Vector2 Offset;

    private Renderer rend;
    private Texture2D noiseText;
    private Color[] colorMap;
    
    void Start() {
        rend = GetComponent<Renderer>();
        noiseText = new Texture2D(MapWidth, MapHeight);
        colorMap = new Color[noiseText.width * noiseText.height];
        
        float[,] map = World.Noise.GenerateNoiseMap(Seed, MapHeight, MapWidth, Scale, Octaves, Persistance, Lacunarity, Offset);
        for (int y = 0; y < MapHeight; y++) {
            for (int x = 0; x < MapWidth; x++) {
                colorMap[y * MapWidth + x] = Color.Lerp(Color.black, Color.white, map[x, y]);
            }
        }
        
        noiseText.SetPixels(colorMap);
        noiseText.Apply();

        rend.sharedMaterial.mainTexture = noiseText;

    }

    void Update() {
    }
}
