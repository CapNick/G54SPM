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

    private Renderer _rend;
    private Texture2D _noiseText;
    private Color[] _colorMap;

    private Terrain _terrain;
    
    void Start() {
        _rend = GetComponent<Renderer>();
        _terrain = GetComponent<Terrain>();
        _terrain.terrainData = GenTerrain(_terrain.terrainData);
        _noiseText = new Texture2D(MapWidth, MapHeight);
        _colorMap = new Color[MapWidth * MapHeight];
        
//        _rend.transform.localScale = new Vector3(MapWidth,1,MapHeight);

    }

    private TerrainData GenTerrain(TerrainData data) {
        float[,] map = World.Noise.GenerateNoiseMap(MapHeight, MapWidth, Seed, Scale, Octaves, Persistance, Lacunarity, Offset);
        data.heightmapResolution = MapWidth + 1;

        data.size = new Vector3(MapWidth, 25,MapHeight);
        
        data.SetHeights(0,0,map);

        return data;

    }

    void FixedUpdate() {
        _terrain.terrainData = GenTerrain(_terrain.terrainData);
        Offset.x += 0.001f;
//        for (int y = 0; y < MapHeight; y++) {
//            for (int x = 0; x < MapWidth; x++) {
//                _colorMap[y * MapWidth + x] = Color.Lerp(Color.black, Color.white, map[x, y]);
//            }
//        }

//        _noiseText.SetPixels(_colorMap);
//        _noiseText.Apply();
//
//        _rend.sharedMaterial.mainTexture = _noiseText;
    }
}
