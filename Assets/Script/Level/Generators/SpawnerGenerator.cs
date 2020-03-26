using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerGenerator : GenerableObject {
    Vector2 gridSize;
    int amount = 4;
    int minBorderMargin = 1;
    int minMarginBetween = 1;

    public List<Spawner> Spawners { get; private set; } = new List<Spawner>();
	public override List<IGameObject> ObjectList => Spawners.ConvertAll(x => (IGameObject)x);

	public SpawnerGenerator(Vector2 gridSize)
    {
        this.gridSize = new Vector2(gridSize.x, gridSize.y);
    }
	
	public override void Create(List<IGameObject> avoidObjects, bool avoidSelf = true)
    {
        for (int i = 0; i < amount; i++)
        {
            Spawner spawner = new Spawner(gridSize, minBorderMargin, minMarginBetween, avoidObjects);
            Spawners.Add(spawner);
			if (avoidSelf) {
				avoidObjects.Add(spawner);
			}
        }

    }

}
