using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuralNetworkVariable
    {
        public enum VariableType
        {
            ZeroToPositiveOne,
            NegativeOneToPositiveOne
        }

        public float Value { get; set; }
        public VariableType Type { get; set; }

        public int Parts { get; set; }

        public NeuralNetworkVariable(float v, VariableType type)
        {
            Value = v;
            Type = type;
            Parts = (int)TypeToIntervalSize() + 1;
        }

        public float TypeToIntervalSize()
        {
            switch (Type)
            {
                case VariableType.ZeroToPositiveOne:
                    return 1;
                case VariableType.NegativeOneToPositiveOne:
                    return 2;
                default:
                    throw new Exception("Incorrect variable type");
            }
        }

    }
}

