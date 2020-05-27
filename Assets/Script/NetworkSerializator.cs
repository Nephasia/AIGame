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
        public static void Load(object o)
        {
            string path = "networks.dat";
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (Stream stream = File.Open(path, FileMode.OpenOrCreate))
            {
                try
                {
                    o = binaryFormatter.Deserialize(stream);
                }
                catch (SerializationException)
                {
                    Debug.Log("Unable to load the networks");
                }

            }
        }

        public static bool HasData()
        {
            string path = "networks.dat";
            return File.Exists(path);
        }



    }
}