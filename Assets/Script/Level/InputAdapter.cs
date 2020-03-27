using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

namespace Game
{
    public class InputAdapter
    {


        static int ConvertToInt(NeuralNetworkVariable nnv)
        {
            float intervalSize = nnv.TypeToIntervalSize();
            float interval = intervalSize / nnv.Parts; // 0.66

            if (nnv.Value < -intervalSize / 2 + interval) // -1 + 0.66
            {
                return -1;
            }
            else if (nnv.Value >= -intervalSize / 2 + interval && nnv.Value <= intervalSize / 2 - interval)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static (int, int, int) ConvertToInput(NeuralNetworkVariable nnv1, NeuralNetworkVariable nnv2, NeuralNetworkVariable nnv3)
        {
            int i1, i2, i3;
            i1 = ConvertToInt(nnv1);
            i2 = ConvertToInt(nnv2);
            i3 = ConvertToInt(nnv3);

            return (i1, i2, i3);
        }
    }
}

