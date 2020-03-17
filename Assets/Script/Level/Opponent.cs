using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent
{
    GameObject GameObject { get; set; }

    int Id { get; set; }

    int LifePoints { get; set; } = 100;

    int KillCount { get; set; } = 0;

    int Points { get; set; } = 0;

    float VisionResolution { get; set; }

    float VisionAngle { get; set; }

    int[] VisionTable { get; set; }

    public Opponent(float visionAngle, float visionResolution)
    {
        int tableSize = ((int)((visionAngle / 2) / visionResolution)) * 2 + 1;
        VisionResolution = visionResolution;
        VisionAngle = visionAngle;
        VisionTable = new int[tableSize];
    }
}
