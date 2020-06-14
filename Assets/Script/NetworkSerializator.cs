using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Game
{
    public static class NetworkSerializator
    {
        public static void Save(object o)
        {
            string path = "networks.dat";
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (Stream stream = File.Open(path, FileMode.Create))
            {
                try
                {
                    binaryFormatter.Serialize(stream, o);
                }
                catch (SerializationException)
                {
                    Debug.Log("Unable to save the networks");
                }

            }
        }
        public static List<AI.NeuralNetwork> Load()
        {
            List<AI.NeuralNetwork> neuralNetworks = null;
            string path = "networks.dat";
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (Stream stream = File.Open(path, FileMode.OpenOrCreate))
            {
                try
                {
                    neuralNetworks = binaryFormatter.Deserialize(stream) as List<AI.NeuralNetwork>;
                }
                catch (SerializationException)
                {
                    Debug.Log("Unable to load the networks");
                }

            }

            return neuralNetworks;

        }

        public static bool HasData()
        {
            string path = "networks.dat";
            return File.Exists(path);
        }



    }
}