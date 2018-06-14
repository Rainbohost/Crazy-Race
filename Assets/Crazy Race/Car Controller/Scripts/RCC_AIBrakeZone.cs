

using UnityEngine;
using System.Collections;

/// <summary>
/// Brake Zones are meant to be used for slowing AI cars. If you have a sharp turn on your scene, you can simply use one of these Brake Zones. It has a target speed. AI will adapt its speed to this target speed while in this Brake Zone. It's simple.
/// </summary>
[AddComponentMenu("X-Zone Games/Car Controller/AI/Brake Zone")]
public class RCC_AIBrakeZone : MonoBehaviour {
	
	public float targetSpeed = 50;
	
}
