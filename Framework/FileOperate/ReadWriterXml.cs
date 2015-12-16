using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Framework.FileOperate
{
    public class ReadWriterXml
    {
        /// <summary>
        /// 读取配置参数数据
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="Section">Scation</param>
        /// <param name="Key">key</param>
        /// <returns></returns>
        public string ReadXml(string Path, string Root, string Section, string Key)
        {

            //XmlDocument是托管资源 不需要你主动释放

            //1.读取book节点
            XmlDocument xmlDoc = new XmlDocument();

            try
            {

                xmlDoc.Load(Path);
                //无重复节点：
                XmlNode xnf = xmlDoc.SelectSingleNode(Root + "/" + Section + "/" + Key); 
                //子节点: 
                return xnf.InnerText;            

            }

            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";

            }
        }


        /// <summary>
        /// 写入配置参数数据
        /// </summary>
        /// <param name="Path"></param>
        public void WriterXml(string Path)
        {


            try
            {
                // 创建XmlTextWriter类的实例对象
                XmlTextWriter textWriter = new XmlTextWriter(Path, null);
                textWriter.Formatting = Formatting.Indented;

                // 开始写过程，调用WriteStartDocument方法
                textWriter.WriteStartDocument();

                // 写入说明
                textWriter.WriteComment("First Comment XmlTextWriter Sample Example");
                textWriter.WriteComment("w3sky.xml in root dir");

                //创建一个节点
                textWriter.WriteStartElement("Administrator");
                textWriter.WriteElementString("Name", "formble");
                textWriter.WriteElementString("site", "w3sky.com");
                textWriter.WriteEndElement();

                // 写文档结束，调用WriteEndDocument方法
                textWriter.WriteEndDocument();

                // 关闭textWriter
                textWriter.Close();

            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
