using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game
{
	public class OpponentsCreator : IObjectList
	{

		public static OpponentsCreator Instance { get; private set; }

		public List<Opponent> Opponents = new List<Opponent>();
		public List<IGameObject> ObjectList => Opponents.ConvertAll(x => (IGameObject)x);

        public List<int> AliveOpponents = new List<int>();

        public OpponentsCreator()
		{
			Instance = null;
			Instance = this;
		}

		public void CreateOpponents(int amount, SpawnerGenerator spawnerGenerator)
		{

			if (CheckIfEnoughSpawners(amount, spawnerGenerator))
			{

				Stack<Vector3> spawnersPositions = new Stack<Vector3>(spawnerGenerator.Spawners.Select(x => x.Position));
				Debug.Log(Opponents.Count);
				for (int i = 0; i < amount; i++)
				{
					Opponent opponent = new Opponent(spawnersPositions.Pop());
					Opponents.Add(opponent);
                    AliveOpponents.Add(opponent.Id);
				}

			}
			else
			{
				throw new System.Exception(
					"Not enought spawners for enemies !\n" +
					"You have " + spawnerGenerator.SpawnersAmount + " spawners only."
				);
			}
		}

        public void CreateOpponents(int amount, SpawnerGenerator spawnerGenerator, List<AI.NeuralNetwork> neuralNetworks)
        {

            if (CheckIfEnoughSpawners(amount, spawnerGenerator))
            {

                Stack<Vector3> spawnersPositions = new Stack<Vector3>(spawnerGenerator.Spawners.Select(x => x.Position));

                for (int i = 0; i < amount; i++)
                {
                    Opponent opponent = new Opponent(spawnersPositions.Pop(), neuralNetworks[i]);
                    Opponents.Add(opponent);
                }

            }
            else
            {
                throw new System.Exception(
                    "Not enought spawners for enemies !\n" +
                    "You have " + spawnerGenerator.SpawnersAmount + " spawners only."
                );
            }
        }

        private bool CheckIfEnoughSpawners(int amount, SpawnerGenerator spawnerGenerator)
		{
			return amount <= spawnerGenerator.SpawnersAmount;
		}

	}

}
