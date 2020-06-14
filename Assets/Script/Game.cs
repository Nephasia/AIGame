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

        public static float deltaTime;
        public float lastTime;

		public Game()
		{
        }

		public void CreateGame()
		{

			if(Updateables.Count > 0) Updateables.Clear();

			LevelBuilder = new LevelBuilder();
			LevelBuilder.BuildLevel();

			LevelBuilder.CreateEnemies();
			LevelBuilder.CreateBullets();

		}

        public void CreateGame(List<AI.NeuralNetwork> neuralNetworks)
        {

            if (Updateables.Count > 0) Updateables.Clear();

            LevelBuilder = new LevelBuilder();
            LevelBuilder.BuildLevel();

            LevelBuilder.CreateEnemies(neuralNetworks);
            LevelBuilder.CreateBullets();

        }

        public void MakeIteration(int iterations = 1)
		{
			for (int i = 0; i < iterations; i++)
			{
				if (iterations == 1) deltaTime = Time.deltaTime;
				else deltaTime = 1/60f;
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

        public List<AI.NeuralNetwork> ExportNeuralNetworks()
        {
            List <Opponent> opponents = new List<Opponent>();
            List<AI.NeuralNetwork> neuralNetworks = new List<AI.NeuralNetwork>();

            foreach (Opponent opponent in LevelBuilder.OpponentsCreator.Opponents)
            {
                opponents.Add(opponent);
            }

            opponents = opponents.OrderByDescending(x => x.Score).ToList();

            UnityEngine.Debug.Log("High Score: " + opponents[0].Score);
            UnityEngine.Debug.Log("Lowest Score: " + opponents[opponents.Count - 1].Score);

            foreach (Opponent opponent in opponents)
            {
                neuralNetworks.Add(opponent.NeuralNetwork);
            }
            return neuralNetworks;
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
