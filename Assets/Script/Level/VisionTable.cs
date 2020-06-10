﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionTable {

    public SeenObjectType[] Table { get; set; }

	public const float VisionAngle = 80;      // in degrees
	const int VisionResolution = 2;   // in degrees
	const float VisionRange = 40;

    public enum SeenObjectType : int {
		None = 0,
		Obstacle,
		Opponent,
		Bullet
	}

    public VisionTable() {
        int tableSize = ((int)((VisionAngle / 2) / VisionResolution)) * 2 + 1;
		Table = new SeenObjectType[tableSize];
    }

    public void Update(Transform currentTransform) {
     
        Table[0] = Look(currentTransform);
        for (int i = 1; i < Table.Length; i++) {

            if(i % 2 == 0) {
                Table[i] = Look(currentTransform, VisionResolution*((i+1)/2));
            } else {
                Table[i] = Look(currentTransform, -VisionResolution*((i+1)/2));
            }
        }

    }

    public SeenObjectType Look(Transform currentTransform, int rotInY = 0) {
        RaycastHit hit;

        if (Physics.Raycast(
                currentTransform.position, 
                currentTransform.TransformDirection(Quaternion.Euler(0, rotInY, 0) * Vector3.forward),
                out hit, 
                VisionRange
            )
        )
        {
            switch (hit.collider.gameObject.name.Substring(4, 4+3))
            {
                case "Opp":
                    return SeenObjectType.Opponent;
                case "Obs":
                    return SeenObjectType.Obstacle;
                case "Bul":
                    return SeenObjectType.Bullet;
                default:
                    return SeenObjectType.None;
            }
        }
        return SeenObjectType.None;
    }


}
