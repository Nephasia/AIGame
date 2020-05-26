using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class Evolution
    {
        public static void NextGeneration(this List<AI.NeuralNetwork> neuralNetworks)
        {
            if (neuralNetworks.Count % 2 == 0)
            {
                for (int i = neuralNetworks.Count / 2; i < neuralNetworks.Count; i++)
                {
                    neuralNetworks[i] = neuralNetworks[i - neuralNetworks.Count / 2].MakeChild();
                    //neuralNetworks[i].MutateWeights();
                }
            }
            else
            {
                for (int i = (neuralNetworks.Count - 1) / 2; i < neuralNetworks.Count - 1; i++)
                {
                    neuralNetworks[i] = neuralNetworks[i - (neuralNetworks.Count - 1) / 2].MakeChild();
                    //neuralNetworks[i].MutateWeights();
                }
                neuralNetworks[neuralNetworks.Count - 1] = neuralNetworks[0].MakeChild();
                //neuralNetworks[neuralNetworks.Count - 1].MutateWeights();
            }
        }


    }
}
