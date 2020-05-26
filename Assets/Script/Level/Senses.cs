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

        public bool IsMoving(Vector3 current, Vector3 last)
        {
            return (current - last).magnitude > 0;
        }

        public float DeltaRotation(Quaternion current, Quaternion last)
        {
            return current.eulerAngles.y - last.eulerAngles.y;
        }

        public Inputs.RotationEnum IsRotating(Quaternion current, Quaternion last) 
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

        //TODO: Ilu przeciwników widzimy
        //TODO: ile stopnii do przeciwnika do którego brakuje najmniej stopnii w obrocie
        //TODO: ile stopnii do przeciwnika najbliżej nas
        //TODO: Czy pocisk leci na nas(widząc go w obrębie kąta widzenia)
    }
}
