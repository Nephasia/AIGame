using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game {

	public LevelBuilder LevelBuilder { get; private set; }

	public Game() {

	}

	public void CreateGame() {

		LevelBuilder = new LevelBuilder();
		LevelBuilder.BuildLevel();

	}

}
