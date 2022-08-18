using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace NetCoreUserService
{
    public class HttpPostWebService
    {
        public SortedDictionary<string,object> PostService(string url,string method,string value)
        {
            string result = string.Empty;
            string param = string.Empty;
            byte[] bytes = null;

            Stream writer = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            param = HttpUtility.UrlEncode("theCityName") + "=" + HttpUtility.UrlEncode(value);

            bytes = Encoding.UTF8.GetBytes(param);

            request = (HttpWebRequest)WebRequest.Create(url + "/" + method);

            request.Method = "POST";

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = bytes.Length;

            try
            {
                writer = request.GetRequestStream(); //获取写入请求的stream
            }
            catch (Exception ex)
            {
                return null;
            }

            writer.Write(bytes, 0, bytes.Length); //把参数数据写入请求数据流
            writer.Close();

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch(Exception ex)
            {
                return null;
            }

            #region 结果字符串
            Stream stream = response.GetResponseStream();  //获取响应流
            XmlTextReader reader = new XmlTextReader(stream);
            reader.MoveToContent();

            result = reader.ReadInnerXml();
            result = $"<xml>{result}</xml>";
            #endregion

            //这种方式读取到的是一个Xml格式的字符串
            //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            //result = reader.ReadToEnd();

            var dic = FromXml(result);

            response.Close();
            response.Dispose();

            reader.Close();
            reader.Dispose();


            stream.Close();
            stream.Dispose();

            return dic;

        }

        public SortedDictionary<string, object> FromXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return new SortedDictionary<string, object>();
            }
            SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            int i = 0;
            foreach (XmlNode xn in nodes)
            {
                i++;
                XmlElement xe = (XmlElement)xn;
                m_values[xe.Name + i] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }
            return m_values;
        }
    }
}
