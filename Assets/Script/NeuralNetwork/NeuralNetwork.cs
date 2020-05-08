using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace AI
{
    public class NeuralNetwork
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
            inputLayer = Matrix<float>.Build.Dense(1, 9);

            //hidden layers
            for (int i = 0; i < hiddenLayerCount; i++)
            {
                Matrix<float> hl = Matrix<float>.Build.Dense(1, neuronCount);
                hiddenLayers.Add(hl);

                //weights
                if (i == 0)
                {
                    //weights from input layer to 1st hidden player
                    Matrix<float> weightsToH1 = Matrix<float>.Build.Dense(9, neuronCount);
                    weights.Add(weightsToH1);
                }
                else
                {
                    //weights from i-1th to ith hidden layer
                    Matrix<float> weightstoHN = Matrix<float>.Build.Dense(neuronCount, neuronCount);
                    weights.Add(weightstoHN);
                }

                //bias
                float los = (float)(RandomNumber.Instance.RandNumber * 2.0f - 1.0f);
                bias.Add(los);
            }

            //output layer
            outputLayer = Matrix<float>.Build.Dense(1, 5);

            //weights from n-1th to output layer
            Matrix<float> weightstoOutput = Matrix<float>.Build.Dense(neuronCount, 5);
            weights.Add(weightstoOutput);

            //output bias
            float los1 = (float)(RandomNumber.Instance.RandNumber * 2.0f - 1.0f);
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
                        float los = (float)(RandomNumber.Instance.RandNumber * 2.0f - 1.0f);
                        weights[k][i, j] = los;
                    }
                }
            }
        }

        public void MutateWeights()
        {
            for (int k = 0; k < weights.Count; k++)
            {
                for (int i = 0; i < weights[k].RowCount; i++)
                {
                    for (int j = 0; j < weights[k].ColumnCount; j++)
                    {
                        float los = (float)(RandomNumber.Instance.RandNumber * 2.0f - 1.0f);
                        weights[k][i, j] = los;
                    }
                }
            }
        }

        public (NeuralNetworkVariable, NeuralNetworkVariable, NeuralNetworkVariable) Learn(float inp1, float inp2, float inp3)
        {
            
            inputLayer[0, 0] = inp1;
            inputLayer[0, 1] = inp2;
            inputLayer[0, 2] = inp3;
            inputLayer[0, 3] = inp3 - 4;
            inputLayer[0, 4] = inp3 *7 ;
            inputLayer[0, 5] = inp3 / 5;
            inputLayer[0, 6] = inp3 - 2;
            inputLayer[0, 7] = inp3;
            inputLayer[0, 8] = inp3;
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

        public NeuralNetwork()
        {
            InitialiseNetwork();
            InitRandomWeights();
        }

        public NeuralNetwork MakeChild(NeuralNetwork n)
        {
            NeuralNetwork child = new NeuralNetwork();
            InitialiseNetwork();


            for (int k = 0; k < weights.Count; k++)
            {
                for (int i = 0; i < weights[k].RowCount; i++)
                {
                    for (int j = 0; j < weights[k].ColumnCount; j++)
                    {
                        float los = (float)Math.Max(0f, Math.Min(1f, (n.weights[k][i, j] + RandomNumber.Instance.RandNumber * 0.01f * 2.0f - 1.0f)));
                        weights[k][i, j] = los;
                    }
                }
            }
            return child;
        }
    }

}
