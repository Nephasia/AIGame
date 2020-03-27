using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class RandomNumber
    {
        Random randomNumber;
        
        public static RandomNumber Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new RandomNumber();
                }
                return _Instance;
            }
        }

        private static RandomNumber _Instance;

        public RandomNumber()
        {
            randomNumber = new Random();
        }

        public float RandNumber => (float)randomNumber.NextDouble();
    }
}
