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
    float ShootTimeCD { get; set; }

    float shootTime = 0.6f;
	const float VisionAngle = 120;		// in degrees
	const float VisionResolution = 1;	// in degrees
	const float VisionRange = 40;

	const float ForwardSpeed = 10;
	const float BackwardSpeed = 6;
	const float AngularSpeed = 60;
	Movement movement;

	public bool CanMove { get; set; } = true;

	public Opponent(Vector3 position)
    {
		Game.RegisterUpdateable(this);
		Id = OponentsCreated++;

		GameObject = new GameObject();
		GameObject.name = this.ToString() + "_" + Id;
		GameObject.transform.position = position;

        int tableSize = ((int)((VisionAngle / 2) / VisionResolution)) * 2 + 1;
        VisionTable = new int[tableSize];
        ShootTimeCD = shootTime;
        movement = new Movement(GameObject, ForwardSpeed, BackwardSpeed, AngularSpeed);

    }

	public void Update() {
		Inputs inputs = SimpleAI();
        ShootTimeCD -= Game.IterationTime;
        if (ShootTimeCD <= 0)
        {
            Shoot();
            ShootTimeCD = shootTime;
        }
		lastPosition = GameObject.transform.position;
		lastRotation = GameObject.transform.rotation;
        
		movement.HandleMovementInput(inputs.MovementType);
		movement.HandleRotationInput(inputs.RotationType);
	}

	private Inputs SimpleAI() {

		float randFloat = Random.Range(0, 1.0f);

		Inputs.MovementEnum mov;

		if (randFloat < 0.7f) {
			mov = Inputs.MovementEnum.Forward;
		} else if(randFloat >= 0.7f && randFloat < 0.8f) {
			mov = Inputs.MovementEnum.Backward;
		} else {
			mov = Inputs.MovementEnum.None;
		}

		Inputs.RotationEnum rot;

		if (randFloat < 0.7f) {
			rot = Inputs.RotationEnum.Left;
		} else if (randFloat >= 0.7f && randFloat < 0.8f) {
			rot = Inputs.RotationEnum.Right;
		} else {
			rot = Inputs.RotationEnum.None;
		}

		Inputs inputs = new Inputs(mov, rot, true);
		return inputs;
	}

	private void Shoot() {
        Bullet bullet = BulletGenerator.Instance.Pop();
        bullet.GameObject.transform.rotation = GameObject.transform.rotation;
        bullet.GameObject.transform.position = GameObject.transform.position;
		bullet.GameObject.transform.Translate(Vector3.forward);
	}

	private void Die() {
		CanMove = false;
		GameObject.transform.position = Vector3.zero;
	}

	public void DealDamage(int damage) {
		LifePoints -= damage;
		if(LifePoints < 0) {
			Die();
		}
	}
	
}
