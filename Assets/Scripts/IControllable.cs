using UnityEngine;
using System.Collections;

public interface IControllable  {
	void Jump(Vector2 force);
    void SpecialJump();
	int JumpInUse { get; }
	Vector3 Position { get;} 
}
