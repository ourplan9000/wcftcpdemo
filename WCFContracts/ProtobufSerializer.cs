using System;
using System.Collections.Generic;
using System.IO;
using ProtoBuf;

namespace WCFContracts
{
    public class ProtobufSerializer
    {

        public object Deserialize(Type type, Stream source)
        {
            return Serializer.Deserialize(type, source);
        }

        public T Deserialize<T>(Stream source)
        {
            return Serializer.Deserialize<T>(source);
        }

        public void Serialize<T>(Stream destination, T instance)
        {
            Serializer.Serialize(destination, instance);
        }
    }

    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class PortoDTO
    {

        public int Number { get; set; }

        public string StrName { get; set; }

        public List<string> lstInfo { get; set; }

        public Dictionary<int, string> DictInfo { get; set; }

    }
}