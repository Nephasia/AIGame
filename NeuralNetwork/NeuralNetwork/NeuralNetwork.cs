using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace NeuralNetwork
{
    class NeuralNetwork
    {
        int hiddenLayerCount;
        int neuronCount;

        public Matrix<float> inputLayer;
        public List<Matrix<float>> hiddenLayers = new List<Matrix<float>>();
        public Matrix<float> outputLayer;

        public List<Matrix<float>> weights = new List<Matrix<float>>();
        public List<float> bias = new List<float>();

        public NeuralNetwork(int hiddenLayerCount, int neuronCount)
        {
            this.hiddenLayerCount = hiddenLayerCount;
            this.neuronCount = neuronCount;
        }

        public void InitialiseNetwork()
        {
            //input layer
            inputLayer = Matrix<float>.Build.Dense(1, 3);

            //hidden layers
            for (int i = 0; i < hiddenLayerCount; i++)
            {
                Matrix<float> hl = Matrix<float>.Build.Dense(1, neuronCount);
                hiddenLayers.Add(hl);

                //weights
                if (i == 0)
                {
                    //weights from input layer to 1st hidden player
                    Matrix<float> weightsToH1 = Matrix<float>.Build.Dense(3, neuronCount);
                    weights.Add(weightsToH1);
                }
                else
                {
                    //weights from i-1th to ith hidden layer
                    Matrix<float> weightstoHN = Matrix<float>.Build.Dense(neuronCount, neuronCount);
                    weights.Add(weightstoHN);
                }

                //bias
                Random r = new Random();
                float los = (float)(r.NextDouble() * 2.0f - 1.0f);
                bias.Add(los);
            }

            //output layer
            outputLayer = Matrix<float>.Build.Dense(1, 5);

            //weights from n-1th to output layer
            Matrix<float> weightstoOutput = Matrix<float>.Build.Dense(neuronCount, 5);
            weights.Add(weightstoOutput);

            //output bias
            Random r1 = new Random();
            float los1 = (float)(r1.NextDouble() * 2.0f - 1.0f);
            bias.Add(los1);
        }

        public void InitRandomWeights()
        {
            for (int k = 0; k < weights.Count; k++)
            {
                for (int i = 0; i < weights[k].RowCount; i++)
                {
                    for (int j = 0; j < weights[k].ColumnCount; j++)
                    {
                        Random r = new Random();
                        float los = (float)(r.NextDouble() * 2.0f - 1.0f);
                        weights[k][i, j] = los;
                    }
                }
            }
        }

        public (NeuralNetworkVariable, NeuralNetworkVariable, NeuralNetworkVariable) Learn(float inp1, float inp2, float inp3)
        {
            InitialiseNetwork();
            InitRandomWeights();

            inputLayer[0, 0] = inp1;
            inputLayer[0, 1] = inp2;
            inputLayer[0, 2] = inp3;
            //make those values in range [-1,1]
            inputLayer = inputLayer.PointwiseTanh();

            //compute hidden layers values
            hiddenLayers[0] = (inputLayer * weights[0] + bias[0]).PointwiseTanh();

            for (int i = 1; i < hiddenLayerCount; i++)
            {
                hiddenLayers[i] = (hiddenLayers[i - 1] * weights[i] + bias[i]).PointwiseTanh();
            }

            //compute output layer
            outputLayer = (hiddenLayers[hiddenLayerCount - 1] * weights[weights.Count - 1] + bias[bias.Count - 1]).PointwiseTanh();

            NeuralNetworkVariable n1 = new NeuralNetworkVariable(outputLayer[0, 0], NeuralNetworkVariable.VariableType.NegativeOneToPositiveOne);
            NeuralNetworkVariable n2 = new NeuralNetworkVariable(outputLayer[0, 1], NeuralNetworkVariable.VariableType.NegativeOneToPositiveOne);
            NeuralNetworkVariable n3 = new NeuralNetworkVariable(Sigmoid(outputLayer[0, 2]), NeuralNetworkVariable.VariableType.ZeroToPositiveOne);
            return (n1,n2,n3);
        }

        public float Sigmoid(double x)
        {
            return (float)(1 / (1 + Math.Exp(-x)));
        }
    }

}
