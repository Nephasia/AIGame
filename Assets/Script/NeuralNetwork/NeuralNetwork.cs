using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;

namespace AI
{
    [Serializable]
    public class NeuralNetwork
    {
        int inputCount;
        int outputCount;
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
            inputLayer = Matrix<float>.Build.Dense(1, inputCount);

            //hidden layers
            for (int i = 0; i < hiddenLayerCount; i++)
            {
                Matrix<float> hl = Matrix<float>.Build.Dense(1, neuronCount);
                hiddenLayers.Add(hl);

                //weights
                if (i == 0)
                {
                    //weights from input layer to 1st hidden player
                    Matrix<float> weightsToH1 = Matrix<float>.Build.Dense(inputCount, neuronCount);
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
            outputLayer = Matrix<float>.Build.Dense(1, outputCount);

            //weights from n-1th to output layer
            Matrix<float> weightstoOutput = Matrix<float>.Build.Dense(neuronCount, outputCount);
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
            //NeuralNetworkVariable n3 = new NeuralNetworkVariable(Sigmoid(outputLayer[0, 2]), NeuralNetworkVariable.VariableType.ZeroToPositiveOne);
            NeuralNetworkVariable n3 = new NeuralNetworkVariable(outputLayer[0, 2], NeuralNetworkVariable.VariableType.ZeroToPositiveOne);
            return (n1,n2,n3);
        }

        public (NeuralNetworkVariable, NeuralNetworkVariable, NeuralNetworkVariable) Learn(VisionTable visionTable)
        {
            float[] inputs = new float[visionTable.Table.Length * 2];

            for (int i = 0; i < visionTable.Table.Length; i++)
            {
                
                if (visionTable.Table[i] == VisionTable.SeenObjectType.Obstacle)
                {
                    inputs[i * 2] = 1.0f;
                }
                if (visionTable.Table[i] == VisionTable.SeenObjectType.Opponent)
                {
                    inputs[i * 2 + 1] = 1.0f;
                }
            }

            return Learn(inputs);
        }

        public (NeuralNetworkVariable, NeuralNetworkVariable, NeuralNetworkVariable) Learn(float[] inputs)
        {
            for (int i = 0; i < inputCount; i++)
            {
                inputLayer[0, i] = inputs[i];
                
            }

            //make those values in range [-1,1]
            //inputLayer = inputLayer.PointwiseTanh();

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
            NeuralNetworkVariable n3 = new NeuralNetworkVariable(outputLayer[0, 2], NeuralNetworkVariable.VariableType.ZeroToPositiveOne);
            return (n1, n2, n3);
        }


        public float Sigmoid(double x)
        {
            return (float)(1 / (1 + Math.Exp(-x)));
        }

        public NeuralNetwork()
        {
            this.hiddenLayerCount = 5;
            this.neuronCount = 60;
            this.inputCount = 54;
            this.outputCount = 5;

            InitialiseNetwork();
            InitRandomWeights();
        }

        public NeuralNetwork MakeChild()
        {
            NeuralNetwork child = new NeuralNetwork();



            for (int k = 0; k < weights.Count; k++)
            {
                for (int i = 0; i < weights[k].RowCount; i++)
                {
                    for (int j = 0; j < weights[k].ColumnCount; j++)
                    {
                        float los = (float)Math.Max(-1f, Math.Min(1f, (weights[k][i, j] + RandomNumber.Instance.RandNumber * 0.1f - RandomNumber.Instance.RandNumber * 0.1f)));
                        child.weights[k][i, j] = los;
                        //child.weights[k][i, j] = weights[k][i, j];
                    }
                }
            }
            return child;
        }
    }

}
