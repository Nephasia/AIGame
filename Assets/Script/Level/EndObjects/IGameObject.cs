using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameObject {

	GameObject GameObject { get; set; }
	Vector3 Position { get; set; }
	Vector3 Scale { get; set; }


}
