/// <summary>
/// NoiseBasic.cs
/// Author: trent (5/16/16)
/// Modified: 
/// 
/// Based on the implementation by Jordan Peck: https://github.com/Auburns/FastNoise
/// </summary>

/// <summary>
/// NoiseBasic Class Definition.
/// 
/// Value noise class.
/// </summary>
public class NoiseBasic : Noise
{
	public static readonly float[] LUT_Value = 
	{
		-0.92125984251968f, 0.826771653543307f, -0.1496062992126f, -0.38582677165354f, 0.039370078740159f, -0.74803149606299f, 0.937007874015748f, 0.858267716535433f, 0.448818897637796f, 0.118110236220474f, -0.73228346456693f, -0.16535433070866f, -0.60629921259842f, 0.637795275590552f, -0.0393700787401601f, 0.354330708661418f, 0.291338582677167f, 0.181102362204726f, -0.7007874015748f, -0.10236220472441f, -0.84251968503937f,
		-0.0551181102362199f, -0.48031496062992f, 0.259842519685041f, 0.165354330708663f, 0.84251968503937f, 0.779527559055119f, -0.81102362204724f, -0.19685039370079f, 0.196850393700789f, 0.669291338582678f, 0.070866141732285f, 0.921259842519685f, -0.18110236220472f, 0.228346456692915f, 0.00787401574803304f, 0.700787401574804f, -0.85826771653543f, 0.023622047244096f, 0.133858267716537f, 0.322834645669293f, -0.93700787401574f,
		-0.29133858267716f, 0.370078740157481f, -0.66929133858267f, -0.96850393700787f, 0.401574803149607f, -0.33858267716535f, -0.4015748031496f, 0.73228346456693f, -0.55905511811023f, -0.43307086614173f, 0.763779527559056f, -0.46456692913386f, 0.543307086614174f, -0.76377952755905f, 0.685039370078741f, 0.748031496062993f, 1.0f, 0.795275590551181f, -0.5748031496063f, -0.54330708661417f, 0.653543307086615f, 0.952755905511811f,
		0.338582677165356f, -0.21259842519685f, 0.433070866141733f, 0.511811023622048f, 0.622047244094489f, -0.90551181102362f, -0.79527559055118f, 0.590551181102363f, -0.11811023622047f, 0.212598425196852f, -0.41732283464567f, -0.95275590551181f, -0.63779527559055f, 0.244094488188978f, 0.30708661417323f, 0.968503937007874f, 0.606299212598426f, -0.77952755905511f, -0.88976377952756f, -0.24409448818897f, -0.32283464566929f,
		-0.71653543307086f, 0.716535433070867f, 0.5748031496063f, 0.086614173228348f, -0.98425196850393f, -0.13385826771653f, -0.51181102362204f, -0.8267716535433f, 0.874015748031496f, -0.35433070866141f, 0.905511811023622f, 0.811023622047244f, -0.68503937007874f, 0.480314960629922f, -0.37007874015748f, -0.2755905511811f, 0.102362204724411f, 0.527559055118111f, -0.44881889763779f, 0.889763779527559f, 0.41732283464567f,
		0.984251968503937f, 0.385826771653544f, -0.87401574803149f, -0.30708661417323f, -0.62204724409449f, -0.52755905511811f, 0.1496062992126f, -0.0866141732283401f, 0.496062992125985f, -0.25984251968504f, -1.0f, -0.59055118110236f, 0.464566929133859f, -0.00787401574802993f, -0.22834645669291f, -0.65354330708661f, -0.49606299212598f, 0.055118110236222f, 0.559055118110237f, -0.07086614173228f, -0.02362204724409f, 0.275590551181104f
	};

	/// <summary>
	/// NoiseBasic Constructor.
	/// </summary>
	public NoiseBasic( )
	{
		noiseType = NoiseType.Value;
	}

	/// <summary>
	/// Get a 2D noise value.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <returns>Result of 2D noise.</returns>
	float GetValue( float x, float y )
	{
		int x0 = FastFloor( x );
		int y0 = FastFloor( y );
		int x1 = x0 + 1;
		int y1 = y0 + 1;

		float xs = 0.0f;
		float ys = 0.0f;
		switch( interpolationType )
		{
			case InterpolationType.Linear:
				xs = x - ( float )x0;
				ys = y - ( float )y0;
			break;

			case InterpolationType.Hermite:
				xs = InterpHermite( x - ( float )x0 );
				ys = InterpHermite( y - ( float )y0 );
			break;

			case InterpolationType.Quintic:
				xs = InterpQuintic( x - ( float )x0 );
				ys = InterpQuintic( y - ( float )y0 );
			break;
		}

		float xf0 = Lerp( LUT_Value[GetLUTIndex( x0, y0 )], LUT_Value[GetLUTIndex( x1, y0 )], xs );
		float xf1 = Lerp( LUT_Value[GetLUTIndex( x0, y1 )], LUT_Value[GetLUTIndex( x1, y1 )], xs );

		return( Lerp( xf0, xf1, ys ) );
	}

	/// <summary>
	/// Get a 3D noise value.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <param name="z">z</param>
	/// <returns>Result of 3D noise.</returns>
	private float GetValue( float x, float y, float z )
	{
		int x0 = FastFloor( x );
		int y0 = FastFloor( y );
		int z0 = FastFloor( z );
		int x1 = x0 + 1;
		int y1 = y0 + 1;
		int z1 = z0 + 1;

		float xs = 0.0f;
		float ys = 0.0f;
		float zs = 0.0f;
		switch( interpolationType )
		{
			case InterpolationType.Linear:
				xs = x - ( float )x0;
				ys = y - ( float )y0;
				zs = z - ( float )z0;
			break;

			case InterpolationType.Hermite:
				xs = InterpHermite( x - ( float )x0 );
				ys = InterpHermite( y - ( float )y0 );
				zs = InterpHermite( z - ( float )z0 );
			break;

			case InterpolationType.Quintic:
				xs = InterpQuintic( x - ( float )x0 );
				ys = InterpQuintic( y - ( float )y0 );
				zs = InterpQuintic( z - ( float )z0 );
			break;
		}

		float xf00 = Lerp( LUT_Value[GetLUTIndex( x0, y0, z0 )], LUT_Value[GetLUTIndex( x1, y0, z0 )], xs );
		float xf10 = Lerp( LUT_Value[GetLUTIndex( x0, y1, z0 )], LUT_Value[GetLUTIndex( x1, y1, z0 )], xs );
		float xf01 = Lerp( LUT_Value[GetLUTIndex( x0, y0, z1 )], LUT_Value[GetLUTIndex( x1, y0, z1 )], xs );
		float xf11 = Lerp( LUT_Value[GetLUTIndex( x0, y1, z1 )], LUT_Value[GetLUTIndex( x1, y1, z1 )], xs );

		float yf0 = Lerp( xf00, xf10, ys );
		float yf1 = Lerp( xf01, xf11, ys );

		return( Lerp( yf0, yf1, zs ) );
	}

	/// <summary>
	/// 2D noise value type method.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <returns>Noise value.</returns>
	public override float GetNoise( float x, float y )
	{
		x*= frequency;
		y*= frequency;

		float sum = 0.0f;
		float max = 1.0f;
		float amp = 1.0f;
		uint i = 0;

		int seedPrev = seed;

		switch( fractalType )
		{
			case FractalType.FBM:
				sum = GetValue( x, y );

				while( ++i < octaves )
				{
					x*= lacunarity;
					y*= lacunarity;

					amp*= gain;
					max+= amp;

					++seed;
					sum+= GetValue( x, y )*amp;
				}
			break;

			case FractalType.Billow:
				sum = FastAbs( GetValue( x, y ) )*2.0f - 1.0f;

				while( ++i < octaves )
				{
					x*= lacunarity;
					y*= lacunarity;
					amp*= gain;
					max+= amp;

					++seed;
					sum+= ( FastAbs( GetValue( x, y ) )*2.0f - 1.0f )*amp;
				}
			break;

			case FractalType.RigidMulti:
				sum = 1.0f - FastAbs( GetValue( x, y ) );

				while( ++i < octaves )
				{
					x*= lacunarity;
					y*= lacunarity;

					amp*= gain;

					++seed;
					sum -= ( 1.0f - FastAbs( GetValue( x, y ) ) )*amp;
				}
			break;
		}

		seed = seedPrev;
		return( sum/max );
	}

	/// <summary>
	/// 3D noise value type method.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <param name="z">z</param>
	/// <returns>Noise value.</returns>
	public override float GetNoise( float x, float y, float z )
	{
		x*= frequency;
		y*= frequency;
		z*= frequency;

		x*= frequency;
		y*= frequency;

		float sum = 0.0f;
		float max = 1.0f;
		float amp = 1.0f;
		uint i = 0;

		int seedPrev = seed;

		switch( fractalType )
		{
			case FractalType.FBM:
				sum = GetValue( x, y, z );

				while( ++i < octaves )
				{
					x*= lacunarity;
					y*= lacunarity;
					z*= lacunarity;

					amp*= gain;
					max+= amp;

					++seed;
					sum+= GetValue( x, y, z )*amp;
				}
			break;

			case FractalType.Billow:
				sum = FastAbs( GetValue( x, y ) )*2.0f - 1.0f;

				while( ++i < octaves )
				{
					x*= lacunarity;
					y*= lacunarity;
					z*= lacunarity;

					amp*= gain;
					max+= amp;

					++seed;
					sum+= ( FastAbs( GetValue( x, y, z ) )*2.0f - 1.0f )*amp;
				}
			break;

			case FractalType.RigidMulti:
				sum = 1.0f - FastAbs( GetValue( x, y, z ) );

				while( ++i < octaves )
				{
					x*= lacunarity;
					y*= lacunarity;
					z*= lacunarity;

					amp*= gain;

					++seed;
					sum -= ( 1.0f - FastAbs( GetValue( x, y, z ) ) )*amp;
				}
			break;
		}

		seed = seedPrev;
		return( sum/max );
	}

	/// <summary>
	/// 4D noise value type method (not implemented, just returns 3D noise value).
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <param name="z">z</param>
	/// <param name="w">w</param>
	/// <returns>Noise value.</returns>
	public override float GetNoise( float x, float y, float z, float w )
	{
		// No implementation; Return 3D noise value.
		// Toss in a console print here eventually.

		// Which means it'll probably never get done. 

		return( GetNoise( x, y, z ) );
	}
}
