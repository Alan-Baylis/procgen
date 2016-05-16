/// <summary>
/// CellularReturnType.cs
/// Author: trent (5/16/16)
/// Modified: 
/// 
/// Based on the implementation by Jordan Peck: https://github.com/Auburns/FastNoise
/// </summary>

/// <summary>
/// Type of cellular return.
/// </summary>
public enum CellularReturnType
{
	CellValue, 
	NoiseLookup,
	DistanceToCenter,
	DistanceToCenterXValue,
	DistanceToCenterSq,
	DistanceToCenterSqXValue,
	DistanceToEdge,
	DistanceToEdgeXValue,
	DistanceToEdgeSq, 
	DistanceToEdgeSqXValue
};
