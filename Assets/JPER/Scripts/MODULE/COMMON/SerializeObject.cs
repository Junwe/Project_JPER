using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;
using System;

public class SerializeObject
{
 public static void Serialize<Object>(Object dictionary, Stream stream)
    {
        try // try to serialize the collection to a file
        {
            using (stream)
            {
                // create BinaryFormatter
                BinaryFormatter bin = new BinaryFormatter();
                // serialize the collection (EmployeeList1) to file (stream)
                bin.Serialize(stream, dictionary);
            }
        }
        catch (IOException)
        {
        }
    }

    public static Object Deserialize<Object>(Stream stream) where Object : new()
    {
        Object ret = CreateInstance<Object>();
        try
        {
            using (stream)
            {
                // create BinaryFormatter
                BinaryFormatter bin = new BinaryFormatter();
                bin.Binder = new VersionDeserializer();
                // deserialize the collection (Employee) from file (stream)
                ret = (Object)bin.Deserialize(stream);
            }
        }
        catch (IOException)
        {
        }
        return ret;
    }
    // function to create instance of T
    public static Object CreateInstance<Object>() where Object : new()
    {
        return (Object)Activator.CreateInstance(typeof(Object));
    }
}

 sealed class VersionDeserializer: SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type deserializeType = null;
            String thisAssembly = Assembly.GetExecutingAssembly().FullName;
            deserializeType = Type.GetType(String.Format("{0}, {1}",
                typeName, thisAssembly));

            return deserializeType;
        }
    }
