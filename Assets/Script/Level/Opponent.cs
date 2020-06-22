using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using System.Linq;
using Math = System.Math;

namespace Game
{
	public class Opponent : IUpdateable, IGameObject
	{

		public GameObject GameObject { get; set; }
        public Vector3 Position
		{
			get => GameObject.transform.position;
			set => GameObject.transform.position = value;
		}
		public Vector3 Scale
		{
			get => GameObject.transform.localScale;
			set => GameObject.transform.localScale = value;
		}

        public Vector3 LastPosition { get; private set; }
		public Quaternion LastRotation { get; private set; }

        public Senses Senses { get; set; }

		static int OponentsCreated = 0;

		public int Id { get; private set; }

        public Weapon Weapon { get; private set; }

		public int LifePoints { get; private set; } = 100;

		public int HitCount { get; private set; } = 0;

		public int KillCount { get; private set; } = 0;

		int leftTurns = 0;
		int rightTurns = 0;

		public int Score
        {
            get
            {
				return
					HitCount * 100 +
					KillCount * 300 +
					(int)(System.Math.Sqrt(Senses.Distance) * 100)+
					LifePoints +
					(int)(System.Math.Sqrt(Senses.Distance * 10)) +
					(int)((leftTurns > rightTurns ? leftTurns / (float)(leftTurns + rightTurns) : rightTurns / (float)(leftTurns + rightTurns)) * 200) +
					HitCount * 5000 / (Weapon.ShootedBullets + 1)
				;
            }
        }

		const float ForwardSpeed = 10;
		const float BackwardSpeed = 10; // was initially 6
		const float AngularSpeed = 90;
		Movement movement;
        public NeuralNetwork NeuralNetwork = new NeuralNetwork();

		public bool IsAlive { get; set; } = true;

		public VisionTable visionTable;

		public Opponent(Vector3 position)
		{
			Game.RegisterUpdateable(this);
			Id = OponentsCreated++;

			GameObject = new GameObject();
			GameObject.name = this.ToString() + "_" + Id;
			GameObject.transform.position = position;
            GameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            LastPosition = Position;
            LastRotation = GameObject.transform.rotation;

            visionTable = new VisionTable();

			movement = new Movement(GameObject, ForwardSpeed, BackwardSpeed, AngularSpeed);
            Weapon = new Weapon();
            Senses = new Senses(movement);
		}

        public Opponent(Vector3 position, NeuralNetwork neuralNetwork) : this(position)
        {
            this.NeuralNetwork = neuralNetwork;
        }

        public void Update()
		{
            Weapon.Reload();
            RefreshStatistics();

			visionTable.Update(GameObject.transform);

            //NeuralNetwork n = new NeuralNetwork(1,10);
			NeuralNetworkVariable n1, n2, n3;
            //(n1, n2, n3) = NeuralNetwork.Learn(0,0,0);
            (n1, n2, n3) = NeuralNetwork.Learn(visionTable);
            int mov, rot, shoot;
			(mov, rot, shoot) = InputAdapter.ConvertToInput(n1, n2, n3);
            //Inputs inputs = SimpleAI();
            Inputs inputs = new Inputs(mov, rot, shoot);

			CheckTurns((Inputs.RotationEnum)System.Enum.Parse(typeof(Inputs.RotationEnum), rot.ToString()));

			LastPosition = Position;
			LastRotation = GameObject.transform.rotation;

            if (IsAlive) {
                movement.HandleMovementInput(inputs.MovementType);
                movement.HandleRotationInput(inputs.RotationType);

                if (inputs.ShootState == Inputs.ShootEnum.Shoot)
                {
                    Weapon.Shoot(GameObject, Id);
                }
            }
        }

		void CheckTurns(Inputs.RotationEnum rotationEnum) {
			switch (rotationEnum) {
				case Inputs.RotationEnum.Left:
					leftTurns++;
					break;
				case Inputs.RotationEnum.Right:
					rightTurns++;
					break;
			}
		}

        private void RefreshStatistics()
        {
            Senses.Distance += (Position - LastPosition).magnitude;
            Senses.IsMoving = Senses.CheckIsMoving(Position, LastPosition);
            Senses.RotationSite = Senses.CheckIsRotating(GameObject.transform.rotation, LastRotation);
			Senses.OponentsSeenAmount = (new List<VisionTable.SeenObjectType>(visionTable.Table)
				.Where(x => x == VisionTable.SeenObjectType.Opponent)).Count();
        }

		float RotationScore(float rotationSitePercentage) {
			float x = rotationSitePercentage;
			return (float)(Math.Cos(x * Math.PI / 2 * Math.Cos(x * 5.5 * Math.Cos(x * Math.PI / 2.03))) * Math.Cos(x / 1.18));
		}

		private Inputs SimpleAI()
		{

			float randFloat = Random.Range(0, 1.0f);

			Inputs.MovementEnum mov;

			if (randFloat < 0.7f)
			{
				mov = Inputs.MovementEnum.Forward;
			}
			else if (randFloat >= 0.7f && randFloat < 0.8f)
			{
				mov = Inputs.MovementEnum.Backward;
			}
			else
			{
				mov = Inputs.MovementEnum.None;
			}

			Inputs.RotationEnum rot;

			if (randFloat < 0.7f)
			{
				rot = Inputs.RotationEnum.Left;
			}
			else if (randFloat >= 0.7f && randFloat < 0.8f)
			{
				rot = Inputs.RotationEnum.Right;
			}
			else
			{
				rot = Inputs.RotationEnum.None;
			}

			Inputs inputs = new Inputs(mov, rot, Inputs.ShootEnum.NoShoot);
			return inputs;
		}

		private void Die()
		{
			IsAlive = false;
            int destroyPosition = 20;
            GameObject.transform.position = new Vector3(destroyPosition, -destroyPosition, destroyPosition);
            OpponentsCreator.Instance.AliveOpponents.Remove(Id);
		}

		public void DealDamage(int damage)
		{
            
            LifePoints -= damage;
			if (LifePoints <= 0)
			{
				Die();
                //Debug.Log(Id + " Died");
			}
		}

		public void AddHitCount(){ 
			HitCount++;	
		}

		public void AddKillCount(){ 
			KillCount++;	
		}

	}
}
