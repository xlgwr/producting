using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Framework.Libs
{
    public class Serial
    {

        /// <summary>
        /// 序列化参数设定
        /// </summary>
        /// <param name="request">要序列化的对象</param>
        /// <param name="strFileName">序列化文件名</param>
        public static void SerializeBinary(object request, string strFileName)
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter serializer =
            new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            FileStream fileStream = new FileStream(strFileName, FileMode.OpenOrCreate);
            serializer.Serialize(fileStream, request);
            fileStream.Close();
            //return memStream.GetBuffer();
        }

        /// <summary>
        /// 反序列化参数设定
        /// </summary>
        /// <param name="strFileName">反序列化文件名</param>
        /// <returns>返回对象</returns>
        public static object DeserializeBinary(string strFileName)
        {
            //System.IO.MemoryStream memStream = new MemoryStream(buf);
            //memStream.Position = 0;
            System.IO.FileStream fileStream = new FileStream(strFileName, FileMode.Open);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter deserializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            object newobj = deserializer.Deserialize(fileStream);
            fileStream.Close();
            return newobj;
        }   
    }
}
