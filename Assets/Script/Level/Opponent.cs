using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using System.Linq;

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

		const float ForwardSpeed = 10;
		const float BackwardSpeed = 10; // was initially 6
		const float AngularSpeed = 60;
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
            Senses.IsMoving = Senses.CheckIsMoving(Position, LastPosition);
            Senses.RotationSite = Senses.CheckIsRotating(GameObject.transform.rotation, LastRotation);
			Senses.OponentsSeenAmount = (new List<VisionTable.SeenObjectType>(visionTable.Table)
				.Where(x => x == VisionTable.SeenObjectType.Opponent)).Count();


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
