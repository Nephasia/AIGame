using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game
{
	public class Game
	{	
        public static LevelBuilder LevelBuilder { get; private set; }

		public static List<IUpdateable> Updateables { get; private set; } = new List<IUpdateable>();

		static float iterationsPerSecond = 60;
		public static float IterationTime { get; private set; }

		public Game()
		{
			IterationTime = 1 / iterationsPerSecond;
		}

		public void CreateGame()
		{

			LevelBuilder = new LevelBuilder();
			LevelBuilder.BuildLevel();

			LevelBuilder.CreateEnemies();
			LevelBuilder.CreateBullets();

		}

        public void CreateGame(List<AI.NeuralNetwork> neuralNetworks)
        {

            LevelBuilder = new LevelBuilder();
            LevelBuilder.BuildLevel();

            LevelBuilder.CreateEnemies(neuralNetworks);
            LevelBuilder.CreateBullets();

        }

        public void MakeIteration(int iterations = 1)
		{
			for (int i = 0; i < iterations; i++)
			{
				IterateGame();
			}
		}

		private void IterateGame()
		{
			for (int i = 0; i < Updateables.Count; i++)
			{
				Updateables[i].Update();
			}
		}

		public static void RegisterUpdateable(IUpdateable updateable)
		{

			if (!Updateables.Contains(updateable))
			{
				Updateables.Add(updateable);
			}

		}

    }
}
