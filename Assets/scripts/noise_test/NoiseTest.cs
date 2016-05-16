/// <summary>
/// NoiseTest.cs
/// Author: trent (5/16/16)
/// Modified: 
/// 
/// Test visualization for the noise classes.
/// </summary>
using UnityEngine;

/// <summary>
/// NoiseTest Class Definition.
/// </summary>
[ExecuteInEditMode]
public class NoiseTest : MonoBehaviour
{
	public NoiseType noiseType;
	public FractalType fractalType;
	public InterpolationType interpolationType;

	[Range( 0, int.MaxValue )]
	public int seed = 0;

	[Range( 2, 4 )]
	public int dimensions = 2;

	[Range( 1, 4 )]
	public uint octaves = 2;

	[Range( 64, 256 )]
	public int resolution = 64;

	[Range( 1, 10 )]
	public int frequency = 10;

	[Range( 0.0f, 2.0f )]
	public float gain = 0.5f;

	[Range( 0.0f, 2.0f )]
	public float lacunarity = 0.5f;

	Texture2D _texture;

	void OnEnable( )
	{
		_texture = new Texture2D( resolution, resolution );
		_texture.hideFlags = HideFlags.DontSave;

		ResetTexture( );
	}

	void OnDisable( )
	{
		DestroyImmediate( _texture );
		_texture = null;
	}

	void Update( )
	{
		ResetTexture( );
	}

	void OnGUI( )
	{
		float w = Screen.height;
		float offset = Screen.width/4;
		GUI.DrawTexture( new Rect( offset, 0, w, w ), _texture );
	}

	void ResetTexture( )
	{
		if( _texture.width != resolution )
			_texture.Resize( resolution, resolution );

		Noise noise = null;
		switch( noiseType )
		{
			case NoiseType.WhiteNoise:
				noise = new NoiseWhite( );
			break;

			case NoiseType.Value:
				noise = new NoiseBasic( );
			break;

			case NoiseType.Gradient:
				noise = new NoiseGradient( );
			break;

			case NoiseType.Simplex:
				noise = new NoiseSimplex( );
			break;

			case NoiseType.Cellular:
				noise = new NoiseCellular( );
				( noise as NoiseCellular ).SetNoiseLookup( new NoiseBasic( ) );
			break;
		}

		noise.SetSeed( seed );

		noise.SetFractalGain( gain );
		noise.SetFractalLacunarity( lacunarity );

		noise.SetFractalType( fractalType );
		noise.SetInterpolationType( interpolationType );

		noise.SetFrequency( frequency );
		noise.SetFractalOctaves( octaves );

		float z = Time.time*0.1f;
		for( int iy = 0; iy < resolution; ++iy )
		{
			float y = ( ( float )iy )/( ( float )resolution );

			for( int ix = 0; ix < resolution; ++ix )
			{
				float x = ( ( float )ix )/( ( float )resolution );

				float c;
				if( dimensions == 2 )
					c = noise.GetNoise( x, y );
				else if( dimensions == 3 )
					c = noise.GetNoise( x, y, z );
				else
					c = noise.GetNoise( x, y, z, noise.GetNoise( x, y, z ) );

				_texture.SetPixel( ix, iy, new Color( c, c, c ) );
			}
		}

		_texture.Apply( );
	}
}
