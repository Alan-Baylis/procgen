using UnityEngine;
using System.Collections;

public class NoiseCellular : MonoBehaviour
{
	protected CellularDistanceFunctionType cellularDistanceFunction;
	protected CellularReturnType cellularReturnType;

	/// <summary>
	/// Set the method used for computing cellular distances.
	/// </summary>
	/// <param name="type">Cellular distance function type.</param>
	public void SetCellularDistanceFunction( CellularDistanceFunctionType type )
	{
		cellularDistanceFunction = type;
	}

	/// <summary>
	/// Set the type returned by cellular noise.
	/// </summary>
	/// <param name="type">Cellular noise return type.</param>
	public void SetCellularReturnType( CellularReturnType type )
	{
		cellularReturnType = type;
	}
}
