using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent
{
    GameObject GameObject { get; set; }

	Vector3 lastPosition;
	Quaternion lastRotation;

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

    public Opponent(float visionAngle, float visionResolution)
    {
        int tableSize = ((int)((visionAngle / 2) / visionResolution)) * 2 + 1;
        VisionTable = new int[tableSize];
    }

	private void ChangePosition() {
		lastPosition = GameObject.transform.position;
		throw new System.NotImplementedException();
	}

	private void ChangeRotation() {
		lastRotation = GameObject.transform.rotation;
		throw new System.NotImplementedException();
	}

	private void WillColide() {
		// todo: collision with environment
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
