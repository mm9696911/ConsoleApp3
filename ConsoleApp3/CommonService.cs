using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public static class CommonService
    {
        /// <summary>
        /// 打开指定路径文件，返回内容字符串
        /// </summary>
        /// <param name="path">指定路径文件</param>
        /// <returns></returns>
        public static string GetFileStream(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            FileInfo fileInfo = new FileInfo(path);
            //创建文件流，path为文本文件路径  
            StreamReader file = new StreamReader(path, Encoding.UTF8);
            string fileText = file.ReadToEnd();
            file.Dispose();
            return fileText;
        }
        public static List<CountryModel> SpiltText(string inputStr)
        {
            List<CountryModel> returnList = new List<CountryModel>();

            foreach (var str in inputStr.Replace("\r\n", "^").Split('^'))
            {
                if (string.IsNullOrEmpty(str)) continue;
                CountryModel model = new CountryModel();
                model.CodeStr = str.Split(':')[0];
                if (string.IsNullOrEmpty(str.Split(':')[1])) continue;
                model.Name = str.Split(':')[1].Substring(3, str.Split(':')[1].LastIndexOf("\"") - 3);

                returnList.Add(model);
            }

            return returnList;
        }

        public static string GenerateCode(List<CountryModel> finnalList)
        {
            string result = string.Empty;
                StringBuilder sb = new StringBuilder();

            foreach (var model in finnalList)
            {
                sb.Append(model.CodeStr + ":0 \"" + model.Name + "\"\r\n");
            }
            result = sb.ToString();
            return result;
        }
    }
}
