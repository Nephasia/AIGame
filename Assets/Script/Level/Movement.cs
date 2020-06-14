using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class Movement
	{
        GameObject GameObject;

		float ForwardSpeed;
		float BackwardSpeed;
		float AngularSpeed;

		public Movement(GameObject instanceObject, float forwardSpeed, float backwardSpeed, float angularSpeed)
		{
			ForwardSpeed = forwardSpeed;
			BackwardSpeed = backwardSpeed;
			AngularSpeed = angularSpeed;
			GameObject = instanceObject;
		}

		public void HandleMovementInput(Inputs.MovementEnum movementType)
		{
			switch (movementType)
			{
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

		public void HandleRotationInput(Inputs.RotationEnum rotationType)
		{
			switch (rotationType)
			{
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

		private void MoveForward()
		{
            if (CanMove(Vector3.forward)) {
                GameObject.transform.Translate(Vector3.forward * ForwardSpeed * Game.deltaTime);
            }
		}

		private void MoveBackward()
		{
            if (CanMove(Vector3.back)) {
                GameObject.transform.Translate(-Vector3.forward * BackwardSpeed * Game.deltaTime);
            }
		}

		private void RotateLeft()
		{
			GameObject.transform.Rotate(Vector3.up, -AngularSpeed * Game.deltaTime);
		}

		private void RotateRight()
		{
			GameObject.transform.Rotate(Vector3.up, AngularSpeed * Game.deltaTime);
		}

        public bool CanMove(Vector3 direction)
        {
            RaycastHit hit;
            
            float length = 1f;

            if (Physics.Raycast(
                GameObject.transform.position, GameObject.transform.TransformDirection(direction),
                out hit, length)
            )
            {
                switch (hit.collider.gameObject.name.Substring(0, hit.collider.gameObject.name.IndexOf("_")))
                {
                    case "Game.Opponent":
                        if(GameObject.name.Substring(0, hit.collider.gameObject.name.IndexOf("_")) == "Game.Opponent") {
                            return false;
                        } else {
                            return true;
                        }
                    case "Game.Obstacle":
                        return false;
                    case "Game.Bullet":
                        return true;
                    case "Game.Spawner":
                        return true;
                    default:
                        break;
                }
            }
            return true;
        }

    }
}
