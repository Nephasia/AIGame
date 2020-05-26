using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

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

        public int Score
        {
            get
            {
                return HitCount * 100 + KillCount * 300 - (int)Senses.Distance;
            }
        }

		int[] VisionTable { get; set; }         // todo: powinna być oddzielna klasa - tablica pozycji

		const float VisionAngle = 120;      // in degrees
		const float VisionResolution = 1;   // in degrees
		const float VisionRange = 40;

		const float ForwardSpeed = 10;
		const float BackwardSpeed = 10; // was initially 6
		const float AngularSpeed = 60;
		Movement movement;
        public NeuralNetwork NeuralNetwork;

		public bool IsAlive { get; set; } = true;

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

            int tableSize = ((int)((VisionAngle / 2) / VisionResolution)) * 2 + 1;
			VisionTable = new int[tableSize];

			movement = new Movement(GameObject, ForwardSpeed, BackwardSpeed, AngularSpeed);

            Weapon = new Weapon();

            Senses = new Senses(movement);

            NeuralNetwork = new NeuralNetwork();


		}

        public Opponent(Vector3 position, NeuralNetwork neuralNetwork)
        {
            Game.RegisterUpdateable(this);
            Id = OponentsCreated++;

            GameObject = new GameObject();
            GameObject.name = this.ToString() + "_" + Id;
            GameObject.transform.position = position;
            GameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            LastPosition = Position;
            LastRotation = GameObject.transform.rotation;

            int tableSize = ((int)((VisionAngle / 2) / VisionResolution)) * 2 + 1;
            VisionTable = new int[tableSize];

            movement = new Movement(GameObject, ForwardSpeed, BackwardSpeed, AngularSpeed);

            Weapon = new Weapon();

            Senses = new Senses(movement);

            this.NeuralNetwork = neuralNetwork;

            

        }

        public void Update()
		{
            Weapon.Reload();
            RefreshStatistics();

            //NeuralNetwork n = new NeuralNetwork(1,10);
			NeuralNetworkVariable n1, n2, n3;
			(n1, n2, n3) = NeuralNetwork.Learn(0,0,0);
			int nn1, nn2, nn3;
			(nn1, nn2, nn3) = InputAdapter.ConvertToInput(n1, n2, n3);
            //Inputs inputs = SimpleAI();
            Inputs inputs = new Inputs(nn1, nn2, nn3);

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

        private void RefreshStatistics()
        {
            Senses.Distance += (Position - LastPosition).magnitude;
            Senses.IsMoving(Position, LastPosition);
            Senses.DeltaRotation(GameObject.transform.rotation, LastRotation);
            Senses.IsRotating(GameObject.transform.rotation, LastRotation);
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
			GameObject.transform.position = new Vector3(0, -20, 0);
            OpponentsCreator.Instance.AliveOpponents.Remove(Id);
		}

		public void DealDamage(int damage)
		{
            
            LifePoints -= damage;
			if (LifePoints <= 0)
			{
				Die();
                Debug.Log(Id + " Died");
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
