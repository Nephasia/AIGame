﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game {

	public Game() {

	}

	public void CreateGame() {

		LevelBuilder levelBuilder = new LevelBuilder();
		levelBuilder.BuildLevel();

	}

}
