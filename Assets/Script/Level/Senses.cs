using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game
{
    public class Senses
    {
        Movement movement;
        public float Distance = 0;
        public bool IsMoving = false;
        public Inputs.RotationEnum RotationSite = Inputs.RotationEnum.None;
        public int OponentsSeenAmount;
        public int MinimumAngleToOpponentToBeSeenInFront;

        public Senses(Movement movement)
        {
            this.movement = movement;
        }

        public bool CanMoveForward()
        {
            return movement.CanMove(Vector3.forward);
        }

        public bool CanMoveBackward()
        {
            return movement.CanMove(Vector3.back);
        }

        public bool CheckIsMoving(Vector3 current, Vector3 last)
        {
            return (current - last).magnitude > 0;
        }

        float DeltaRotation(Quaternion current, Quaternion last)
        {
            return current.eulerAngles.y - last.eulerAngles.y;
        }

        public Inputs.RotationEnum CheckIsRotating(Quaternion current, Quaternion last) 
        {
            int precision = 1;
            if(DeltaRotation(current, last) >= precision) {
                return Inputs.RotationEnum.Right;
            } else if(DeltaRotation(current, last) <= precision) {
                return Inputs.RotationEnum.Left;
            } else {
                return Inputs.RotationEnum.None;
            }
        }

        //TODO: przemyśleć jak wykorzystać
        public bool IsVisibleByEnemy(Opponent current, Opponent opponent)
        {
            Vector3 directionFromOpponentToCurrent = (current.Position - opponent.Position).normalized;
            float dotProd = Vector3.Dot(directionFromOpponentToCurrent, opponent.GameObject.transform.forward);
            //mostly forward
            if (dotProd > 0.9) {
                return true;
            }
            return false;
        }

        public int CheckMinimumAngleToOpponentToBeSeenInFront(VisionTable.SeenObjectType[] visionTable) {

            for (int i = 0; i < visionTable.Length; i++) {
                if(visionTable[i] == VisionTable.SeenObjectType.Opponent)
                    return (i+1)/2;
            }
            return (int)(VisionTable.VisionAngle/2);
        }

        //TODO: ile stopnii do przeciwnika najbliżej nas
        //TODO: Czy pocisk leci na nas(widząc go w obrębie kąta widzenia)
    }
}
