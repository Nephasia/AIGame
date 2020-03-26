using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : IUpdateable, IGameObject {

	public GameObject GameObject { get; set; }
	public Vector3 Position {
		get => GameObject.transform.position;
		set => GameObject.transform.position = value;
	}
	public Vector3 Scale {
		get => GameObject.transform.localScale;
		set => GameObject.transform.localScale = value;
	}

	Vector3 lastPosition;
	Quaternion lastRotation;

	static int OponentsCreated = 0;

    int Id { get; set; }

    int LifePoints { get; set; } = 100;

    int KillCount { get; set; } = 0;

    int Score { get; set; } = 0;

    int[] VisionTable { get; set; }			// todo: powinna być oddzielna klasa - tablica pozycji
	
	const float VisionAngle = 120;		// in degrees
	const float VisionResolution = 1;	// in degrees
	const float VisionRange = 40;

	const float ForwardSpeed = 10;
	const float BackwardSpeed = 6;
	const float AngularSpeed = 60;
	Movement movement;

    public Opponent(Vector3 position)
    {
		Game.RegisterUpdateable(this);
		Id = OponentsCreated++;

		GameObject = new GameObject();
		GameObject.name = this.ToString() + Id;
		GameObject.transform.position = position;

        int tableSize = ((int)((VisionAngle / 2) / VisionResolution)) * 2 + 1;
        VisionTable = new int[tableSize];

		movement = new Movement(GameObject, ForwardSpeed, BackwardSpeed, AngularSpeed);

    }

	public void Update() {
		//Inputs inputs = SimpleAI();

		//lastPosition = GameObject.transform.position;
		//lastRotation = GameObject.transform.rotation;

		//movement.HandleMovementInput(inputs.MovementType);
		//movement.HandleRotationInput(inputs.RotationType);

	}

	private Inputs SimpleAI() {
		throw new System.NotImplementedException();
	}

	private void Shoot() {
		// todo : shoot - create new object as bullet
		// set it in opponent position and rotation
		// keep in mind bullets shoult not collide with us as opponent - maybe place it little ahead of us when creating
		throw new System.NotImplementedException();
	}

	private void Die() {
		throw new System.NotImplementedException();
	}

	private void DealDamage(int damage) {
		LifePoints -= damage;
		if(LifePoints < 0) {
			Die();
		}
	}
	
}
