using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs {

	public enum MovementEnum {
		None = 0,
		Forward,
		Backward
	}

	public enum RotationEnum {
		None = 0,
		Left,
		Right
	}

	public bool Shoot { get; }
	public MovementEnum MovementType { get; }
	public RotationEnum RotationType { get; }

	public Inputs(MovementEnum movementType, RotationEnum rotationType, bool shoot) {
		this.MovementType = movementType;
		this.RotationType = rotationType;
		this.Shoot = shoot;
	}

}
