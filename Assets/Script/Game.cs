using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Diagnostics;

namespace Game
{
	public class Game
	{	
        public static LevelBuilder LevelBuilder { get; private set; }

		public static List<IUpdateable> Updateables { get; private set; } = new List<IUpdateable>();

		static float iterationsPerSecond = 1;
        public static float deltaTime;
        public float lastTime;
        Stopwatch watch;
		public static float IterationTime { get; private set; }

		public Game()
		{
            watch = new Stopwatch();
            watch.Start();
			IterationTime = uint.Parse(PreferencesScript.iterSpeed) / iterationsPerSecond;
            lastTime = (float)watch.Elapsed.TotalSeconds;
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
                if (i == 0) deltaTime = 0.01f;
                else
                {
                    deltaTime = (float)watch.Elapsed.TotalSeconds - lastTime;
                }
                lastTime = (float)watch.Elapsed.TotalSeconds;
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
