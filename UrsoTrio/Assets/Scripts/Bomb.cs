using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BombType
{
	None,
	Column,
	Row,
	Adajcent,
	Color
}

public class Bomb : GamePiece 
{
	public BombType bombType;
}
