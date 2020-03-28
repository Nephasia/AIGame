using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game
{
    public class Statistics
    {
        Movement movement;
        public float Distance = 0;
        

        public Statistics(Movement movement)
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

        //TODO: delta rotation - czy się obracamy; w którą strone: lewo, wcale, prawo; delta; 
        //TODO: ile stopnii do przeciwnika do którego brakuje najmniej stopnii w obrocie
        //TODO: ile stopnii do przeciwnika najbliżej nas
        //TODO: Ilu przeciwników widzimy
        //TODO: Czy przeciwnik jest obrócony wprost na nas
        //TODO: Czy pocisk leci na nas(widząc go w obrębie kąta widzenia)
        //TODO:Czy jest przeszkoda na linii strzału
        //TODO:Czy jest przeciwnik na linii strzału


    }
}
