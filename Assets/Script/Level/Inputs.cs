using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	public class Inputs
	{

		public enum MovementEnum
		{
			Backward= - 1,
			None,
			Forward
		}

		public enum RotationEnum
		{
			Left = -1,
			None,
			Right
		}

		public enum ShootEnum
		{
			NoShoot = 0,
			Shoot
		}

		public ShootEnum ShootState { get; }
		public MovementEnum MovementType { get; }
		public RotationEnum RotationType { get; }

		public Inputs(MovementEnum movementType, RotationEnum rotationType, ShootEnum shoot)
		{
			this.MovementType = movementType;
			this.RotationType = rotationType;
			this.ShootState = shoot;
		}

		public Inputs(int i1, int i2, int i3): this((MovementEnum)i1, (RotationEnum)i2, (ShootEnum)i3)
		{
			
		}

	}
}
