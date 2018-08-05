using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = ConfigurationManager.AppSettings["fileName"].ToString();
            string oldCountriesNameStr = CommonService.GetFileStream(Directory.GetCurrentDirectory() + "\\localisation\\" + fileName + ".yml");
            List<CountryModel> oldCountriesNameList = CommonService.SpiltText(oldCountriesNameStr);

            string newCountriesNameStr = CommonService.GetFileStream(Directory.GetCurrentDirectory() + "\\localisationnew\\" + fileName + ".yml");
            List<CountryModel> newCountriesNameList = CommonService.SpiltText(newCountriesNameStr);

            List<CountryModel> finnalList = new List<CountryModel>();
            foreach (var model in newCountriesNameList)
            {
                var oldModel = oldCountriesNameList.FirstOrDefault(c => c.CodeStr == model.CodeStr);
                if (oldModel != null)
                {
                    if (oldModel.Name.Length > 3 & oldModel.Name.Substring(0, 2) == "王国")
                    {
                        oldModel.Name = oldModel.Name.Substring(2, oldModel.Name.Length - 2) + oldModel.Name.Substring(0, 2);
                    }
                    else if (oldModel.Name.Length > 4 )
                    {
                        if (oldModel.Name.Substring(0, 3) == "共和国")
                        {
                            oldModel.Name = oldModel.Name.Substring(3, oldModel.Name.Length - 3) + oldModel.Name.Substring(0, 3);
                        }
                        else if (oldModel.Name.Length > 5)
                        {
                            if (oldModel.Name.Substring(0, 4) == "伊斯兰国")
                            {
                                oldModel.Name = oldModel.Name.Substring(4, oldModel.Name.Length - 4) + oldModel.Name.Substring(0, 4);
                            }
                            else if (oldModel.Name.Length > 6)
                            {
                                if (oldModel.Name.Substring(0, 5) == "人民共和国")
                                {
                                    oldModel.Name = oldModel.Name.Substring(5, oldModel.Name.Length - 5) + oldModel.Name.Substring(0, 5);
                                }
                                else if (oldModel.Name.Length > 7)
                                {
                                    if (oldModel.Name.Substring(0, 6) == "伊斯兰共和国")
                                        oldModel.Name = oldModel.Name.Substring(6, oldModel.Name.Length - 6) + oldModel.Name.Substring(0, 6);
                                }
                            }
                        }
                    }


                    model.Name = oldModel.Name;
                }

                finnalList.Add(model);
            }

            string fileStr = CommonService.GenerateCode(finnalList);

            string savePath = Directory.GetCurrentDirectory() + "\\localisation2\\" + fileName + ".yml";

            StreamWriter sw = null;
            FileInfo file = new FileInfo(savePath);
            if (!file.Exists)
            {
                FileStream fs = File.Create(savePath);  //创建文件
                fs.Close();
            }

            UTF8Encoding utf8 = new UTF8Encoding(false);

            using (sw = new StreamWriter(savePath, false, utf8))
            {
                sw.Write(fileStr);
            }
        }
    }
}
