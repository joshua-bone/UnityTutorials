using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MazeDirections {
	public const int Count = 4;

	public static MazeDirection RandomValue{
		get {
			return (MazeDirection)Random.Range (0, Count);
		}
	}

	private static IntVector2[] vectors = {
		new IntVector2(0, 1),
		new IntVector2(1, 0),
		new IntVector2(0, -1),
		new IntVector2(-1, 0)
	};

//	An extension method is a static method inside a static class that behaves 
//	like an instance method of some type. That type could be anything, a class, 
//	an interface, a struct, a primitive value, or an enum. The first argument 
//	of an extension method needs to have the this keyword and defines the type 
//	and instance value that the method will operate on.
//	Does this allow us to add methods to everything? Yes, just like you could write any 
//	static method that has any type as its argument. Is this a good idea? When used in 
//	moderation, it can be. It's a specialized tool that has its uses, but wielded it 
//	with abandon will result in an unstructured mess.

	public static IntVector2 ToIntVector2 (this MazeDirection direction) {
		return vectors[(int)direction];
	}

	private static MazeDirection[] opposites = {
		MazeDirection.South,
		MazeDirection.West,
		MazeDirection.North,
		MazeDirection.East
	};

	public static MazeDirection GetOpposite (this MazeDirection direction) {
		return opposites[(int)direction];
	}
}
