/// <summary>
/// NoiseType.cs
/// Author: trent (5/16/16)
/// Modified: 
/// 
/// Based on the implementation by Jordan Peck: https://github.com/Auburns/FastNoise
/// </summary>

/// <summary>
/// Noise Class Definition.
/// 
/// Base noise class for all subsequent noise types.
/// </summary>
public abstract class Noise
{
	public const int kLUTMask = 127;

	protected int seed = 0;

	protected float frequency = 0.01f;

	protected InterpolationType interpolationType = InterpolationType.Quintic;
	protected NoiseType noiseType = NoiseType.Value;

	protected uint octaves = 3;
	protected float lacunarity = 2.0f;
	protected float gain = 0.5f;

	protected FractalType fractalType = FractalType.FBM;

	/// <summary>
	/// Set the noise seed value.
	/// </summary>
	/// <param name="_seed">Noise seed value.</param>
	public void SetSeed( int _seed ) 
	{ 
		seed = _seed;
	}

	/// <summary>
	/// Get the noise seed value.
	/// </summary>
	/// <returns>Noise seed value.</returns>
	public int GetSeed( ) 
	{ 
		return seed; 
	}

	/// <summary>
	/// Set the noise frequency.
	/// </summary>
	/// <param name="_frequency">Noise frequency.</param>
	public void SetFrequency( float _frequency ) 
	{ 
		frequency = _frequency; 
	}

	/// <summary>
	/// Set the noise interpolation type.
	/// </summary>
	/// <param name="type">Noise interpolation type.</param>
	public void SetInterpolationType( InterpolationType type ) 
	{
		interpolationType = type;
	}

	/// <summary>
	/// Set the noise type (this is likely going to be removed).
	/// </summary>
	/// <param name="type">Noise type.</param>
	void SetNoiseType( NoiseType type )
	{ 
		noiseType = type; 
	}

	/// <summary>
	/// Set the number of fractal octaves.
	/// </summary>
	/// <param name="_octaves">Number of octaves.</param>
	public void SetFractalOctaves( uint _octaves )
	{ 
		octaves = _octaves; 
	}

	/// <summary>
	/// Set the noise lacunarity value.
	/// </summary>
	/// <param name="_lacunarity">Lacunarity value.</param>
	public void SetFractalLacunarity( float _lacunarity ) 
	{ 
		lacunarity = _lacunarity; 
	}

	/// <summary>
	/// Set the noise gain value.
	/// </summary>
	/// <param name="_gain">Noise gain value.</param>
	public void SetFractalGain( float _gain ) 
	{ 
		gain = _gain; 
	}

	/// <summary>
	/// Set the fractal type.
	/// </summary>
	/// <param name="_fractalType">Noise fractal type.</param>
	public void SetFractalType( FractalType _fractalType ) 
	{ 
		fractalType = _fractalType; 
	}

	/// <summary>
	/// Utility methods.
	/// </summary>
	protected static int FastFloor( float f ) { return ( f >= 0.0f ? ( int )f : ( int )f - 1 ); }
	protected static int FastRound( float f ) { return ( f >= 0.0f ) ? ( int )( f + 0.5f ) : ( int )( f - 0.5f ); }
	protected static float FastAbs( float f ) { return ( f >= 0.0f ) ? f : -f; }
	protected static int FastAbs( int i ) { return ( i > 0 ) ? i : -i; }
	protected static float Lerp( float a, float b, float t ) { return a + t * ( b - a ); }

	/// <summary>
	/// Linear intepolation function.
	/// </summary>
	/// <param name="o">Output of the linear interpolation (three-dimensional vector).</param>
	/// <param name="a">Vector a (three-dimensional vector).</param>
	/// <param name="b">Vector b (three-dimensional vector).</param>
	/// <param name="t">Interpolant.</param>
	protected static void LerpVector3( out float[] o, float[] a, float[] b, float t )
	{
		o = new float [3];
		o[0] = Lerp( a[0], b[0], t);
		o[1] = Lerp( a[1], b[1], t);
		o[2] = Lerp( a[2], b[2], t);
	}

	/// <summary>
	/// Hermite interpolation method.
	/// </summary>
	/// <param name="t">Interpolant.</param>
	/// <returns>Interpolated value.</returns>
	protected static float InterpHermite( float t ) 
	{ 
		return( t*t*( 3 - 2 * t ) ); 
	}

	/// <summary>
	/// Quintic interpolation method.
	/// </summary>
	/// <param name="t">Interpolant.</param>
	/// <returns>Interpolated value.</returns>
	protected static float InterpQuintic( float t ) 
	{ 
		return( t*t*t*( t*( t * 6 - 15 ) + 10 ) ); 
	}

	/// <summary>
	/// Get the look-up table index for a 2D coordinate.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <returns>Look-up table index.</returns>
	protected int GetLUTIndex( int x, int y )
	{
		int hash = seed;
		hash^= x;
		hash*= 15485863;
		hash^= y;
		hash*= 10057189;
		hash^= hash >> 16;

		return( hash & kLUTMask );
	}

	/// <summary>
	/// Get the look-up table index for a 3D coordinate.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <param name="z">z</param>
	/// <returns>Look-up table index.</returns>
	protected int GetLUTIndex( int x, int y, int z )
	{
		int hash = seed;
		hash^= x;
		hash*= 15485863;
		hash^= y;
		hash*= 10057189;
		hash^= z;
		hash*= 987391;
		hash^= hash >> 16;

		return( hash & kLUTMask );
	}

	/// <summary>
	/// Get the look-up table index for a 4D coordinate.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <param name="z">z</param>
	/// <param name="w">w</param>
	/// <returns>Look-up table index.</returns>
	protected int GetLUTIndex( int x, int y, int z, int w )
	{
		int hash = seed;
		hash^= x;
		hash*= 15485863;
		hash^= y;
		hash*= 10057189;
		hash^= z;
		hash*= 987391;
		hash^= w;
		hash*= 418493;
		hash^= hash >> 16;

		return( hash & kLUTMask );
	}

	// Abstract noise methods. 
	public abstract float GetNoise( float x, float y );
	public abstract float GetNoise( float x, float y, float z );
	public abstract float GetNoise( float x, float y, float z, float w );
}
