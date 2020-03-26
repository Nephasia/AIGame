using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Game {

	public LevelBuilder LevelBuilder { get; private set; }

	static HashSet<IUpdateable> Updateables = new HashSet<IUpdateable>();

	static float iterationsPerSecond = 60;
	public static float IterationTime { get; private set; }

	public Game() {
		IterationTime = 1 / iterationsPerSecond;
	}

	public void CreateGame() {

		LevelBuilder = new LevelBuilder();
		LevelBuilder.BuildLevel();

        LevelBuilder.CreateEnemies();
        

    }

	public void MakeIteration(int iterations = 1) {
		for (int i = 0; i < iterations; i++) {
			IterateGame();
		}
	}

	private void IterateGame() {
		for (int i = 0; i < Updateables.Count; i++) {
			Updateables.ElementAt(i).Update();
		}
	}

	public static void RegisterUpdateable(IUpdateable updateable) {
		Updateables.Add(updateable);
	}


}
