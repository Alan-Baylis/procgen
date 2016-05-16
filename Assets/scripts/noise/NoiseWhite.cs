/// <summary>
/// NoiseWhite.cs
/// Author: trent (5/16/16)
/// Modified: 
/// 
/// Based on the implementation by Jordan Peck: https://github.com/Auburns/FastNoise
/// </summary>

/// <summary>
/// NoiseWhite Class Definition.
/// 
/// White noise class.
/// </summary>
public class NoiseWhite : Noise
{
	/// <summary>
	/// Basic 2D white noise using integer coordinates.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <returns>Noise value.</returns>
	public float GetNoiseInt( int x, int y )
	{
		return( NoiseBasic.LUT_Value[GetLUTIndex( x, y )] );
	}

	/// <summary>
	/// Basic 3D white noise using integer coordinates.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <param name="z">z</param>
	/// <returns>Noise value.</returns>
	public float GetNoiseInt( int x, int y, int z )
	{
		return( NoiseBasic.LUT_Value[GetLUTIndex( x, y, z )] );
	}

	/// <summary>
	/// Basic 4D white noise using integer coordinates.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <param name="z">z</param>
	/// <param name="w">w</param>
	/// <returns>Noise value.</returns>
	public float GetNoiseInt( int x, int y, int z, int w )
	{
		return( NoiseBasic.LUT_Value[GetLUTIndex( x, y, z, w )] );
	}

	/// <summary>
	/// Basic 2D white noise using floating-point coordinates.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <returns>Noise value.</returns>
	public override float GetNoise( float x, float y )
	{
		return( NoiseBasic.LUT_Value[GetLUTIndex( ( ( int )x )^( ( ( int )x ) >> 16 ),
												  ( ( int )y )^( ( ( int )y ) >> 16 ) )] );
	}

	/// <summary>
	/// Basic 3D white noise using floating-point coordinates.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <param name="z">z</param>
	/// <returns>Noise value.</returns>
	public override float GetNoise( float x, float y, float z )
	{
		return( NoiseBasic.LUT_Value[GetLUTIndex( ( ( int )x )^( ( ( int )x ) >> 16 ),
												  ( ( int )y )^( ( ( int )y ) >> 16 ),
												  ( ( int )z )^( ( ( int )z ) >> 16 ) )] );
	}

	/// <summary>
	/// Basic 4D white noise using floating-point coordinates.
	/// </summary>
	/// <param name="x">x</param>
	/// <param name="y">y</param>
	/// <param name="z">z</param>
	/// <param name="w">w</param>
	/// <returns>Noise value.</returns>
	public override float GetNoise( float x, float y, float z, float w )
	{
		return( NoiseBasic.LUT_Value[GetLUTIndex( ( ( int )x )^( ( ( int )x ) >> 16 ),
												  ( ( int )y )^( ( ( int )y ) >> 16 ),
												  ( ( int )z )^( ( ( int )z ) >> 16 ),
												  ( ( int )w )^( ( ( int )w ) >> 16 ) )] );
	}
}
