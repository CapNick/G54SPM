using System.Collections.Generic;
using UnityEngine;
using World;


public class NoiseTests : MonoBehaviour {

	[Header("Surface Generation")] 
	public int Seed;
	[Range(1, 8)]
	public int SurfaceOctaves;
	[Range(1f,4f)]
	public float SurfaceLacunarity;
	[Range(0f,1f)]
	public float SurfaceFrequency;
	public float SurfaceGain;

		
	public FastNoise.NoiseType SurfaceNType;
	public FastNoise.FractalType SurfaceFType;
	public FastNoise.Interp SurfaceInterp;
	
	[Header("Other")]
	public int Width;
	public int Height;
	public int Scale;
	public Vector2 Offset;
	
	
	public FastNoise Noise;
	private Renderer _renderer;
	private Texture2D _texture;
	private Color[] _pixels;

	private float upDown = 0;
	
	// Use this for initialization
	void Awake () {
		_renderer = GetComponent<Renderer>();
		_texture = new Texture2D(Width,Height);
		_pixels = new Color[Width * Height];
		_renderer.material.mainTexture = _texture;
		
		Noise = new FastNoise(Seed);
		Noise.SetNoiseType(SurfaceNType);
		Noise.SetFractalType(SurfaceFType);
		Noise.SetFractalOctaves(SurfaceOctaves);
		Noise.SetFractalLacunarity(SurfaceLacunarity);
		Noise.SetFractalGain(SurfaceGain);
		Noise.SetFrequency(SurfaceFrequency);
		Noise.SetInterp(SurfaceInterp);
//		_noise.
		CalculateNoise();
	}

	public void CalculateNoise() {
		float min = 0f;
		float max = 0f;

//		if (upDown > 1) {
//			upDown = 0;
//		}
//		else {
//			upDown += 0.0625f;
//		}
		
		
		float y = 0.0F;
		while (y < _texture.height) {
			float x = 0.0F;
			while (x < _texture.width) {
				float xCoord = (Offset.x + x / _texture.width);
				float yCoord = (Offset.y + y / _texture.height);
				float sample = Noise.GetNoise(xCoord,upDown,yCoord);

				if (sample < min) {
					min = sample;
				}
				
				if (sample > max) {
					max = sample;
				}
		
				if (sample > 0.5) {
					_pixels[(int)y * _texture.width + (int)x] = new Color(sample,sample,sample);
				}
				else if (sample > 0.4) {
					_pixels[(int)y * _texture.width + (int)x] = new Color(sample,sample,sample);
				}
				else if (sample > 0.3) {
					_pixels[(int)y * _texture.width + (int)x] = new Color(0,0.9f,0);
				}
				else if (sample > 0.2) {
					_pixels[(int)y * _texture.width + (int)x] = new Color(0,0.8f,0);
				}
				else if (sample > 0.01) {
					_pixels[(int)y * _texture.width + (int)x] = new Color(0,0.7f,0);
				}
				else if (sample > 0) {
					_pixels[(int)y * _texture.width + (int)x] = new Color(1f, 1f, 0.2f);
				}
				else {
					_pixels[(int)y * _texture.width + (int)x] = new Color(0,0,1f);
				}
				
				
				
				x++;
			}
			y++;
		}
		
//		Debug.Log("Min: "+min);
//		Debug.Log("Max: "+max);
		_texture.SetPixels(_pixels);
		_texture.Apply();
	}
	
	
//	// Update is called once per frame
//	public void Update () {
//		if (Input.GetKeyDown(KeyCode.UpArrow)) {
//			upDown += 0.0625f;
//			CalculateNoise();
//		}
//		if (Input.GetKeyDown(KeyCode.DownArrow)) {
//			upDown -= 0.0625f;
//			CalculateNoise();
//		}
////
//		if (!Input.GetMouseButton(0))
//			return;
//
//		RaycastHit hit;
//		if (!Physics.Raycast(		Camera.main
//			.ScreenPointToRay(Input.mousePosition), out hit))
//			return;
//
//		Renderer rend = hit.transform.GetComponent<Renderer>();
//		MeshCollider meshCollider = hit.collider as MeshCollider;
//
//		if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
//			return;
//
//		Texture2D tex = rend.material.mainTexture as Texture2D;
//		Vector2 pixelUV = hit.textureCoord;
//		pixelUV.x *= tex.width;
//		pixelUV.y *= tex.height;
//		Debug.Log("("+Mathf.Abs(_texture.height - pixelUV.x)+", "+Mathf.Abs(_texture.width - pixelUV.y)+")");
//		Debug.Log(Noise.GetNoise(Mathf.Abs(_texture.height - pixelUV.x)+Offset.x,Mathf.Abs(_texture.width - pixelUV.y)+Offset.y));
//
//		
//
////		CalculateNoise();
//	}

	public void OnValidate() {
		if (Noise != null) {
			Noise.SetNoiseType(SurfaceNType);
			Noise.SetFractalType(SurfaceFType);
			Noise.SetFractalOctaves(SurfaceOctaves);
			Noise.SetFractalLacunarity(SurfaceLacunarity);
			Noise.SetFractalGain(SurfaceGain);
			Noise.SetFrequency(SurfaceFrequency);
			Noise.SetInterp(SurfaceInterp);
			CalculateNoise();
		}
		
	}
}
