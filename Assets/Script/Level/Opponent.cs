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

    float shootTime = 1;
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
		
        Inputs inputs = new Inputs(Inputs.MovementEnum.Forward, Inputs.RotationEnum.Left, true);

		return inputs;
	}

	private void Shoot() {
        Bullet bullet = BulletGenerator.Instance.Pop();
        bullet.GameObject.transform.rotation = GameObject.transform.rotation;
        bullet.GameObject.transform.position = GameObject.transform.position;
        Debug.Log(bullet.Position);
        GameObject.transform.Translate(Vector3.forward);
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
