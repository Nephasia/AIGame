﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement {

	GameObject GameObject;

	float ForwardSpeed;
	float BackwardSpeed;
	float AngularSpeed;

	public Movement(GameObject instanceObject, float forwardSpeed, float backwardSpeed, float angularSpeed) {
		ForwardSpeed = forwardSpeed;
		BackwardSpeed = backwardSpeed;
		AngularSpeed = angularSpeed;
		GameObject = instanceObject;
	}

	public void HandleMovementInput(Inputs.MovementEnum movementType) {
		switch (movementType) {
			case Inputs.MovementEnum.Forward:
				MoveForward();
				break;
			case Inputs.MovementEnum.Backward:
				MoveBackward();
				break;
			default:
				// do nothing
				break;
		}
	}

	public void HandleRotationInput(Inputs.RotationEnum rotationType) {
		switch (rotationType) {
			case Inputs.RotationEnum.Left:
				RotateLeft();
				break;
			case Inputs.RotationEnum.Right:
				RotateRight();
				break;
			default:
				// do nothing
				break;
		}
	}

	private void MoveForward() {
		GameObject.transform.Translate(Vector3.forward * ForwardSpeed * Game.IterationTime);
	}

	private void MoveBackward() {
		GameObject.transform.Translate(-Vector3.forward * BackwardSpeed * Game.IterationTime);
	}

	private void RotateLeft() {
		GameObject.transform.Rotate(Vector3.up, -AngularSpeed * Game.IterationTime);
	}

	private void RotateRight() {
		GameObject.transform.Rotate(Vector3.up, AngularSpeed * Game.IterationTime);
	}

}
