using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DynamicsExtensions.Extensions
{
    public static class MvcExtensions
    {
        public static string[] ReadAllLines(this HttpPostedFileBase httpPostedFileBase)
        {
            List<string> lines = new List<string>();
            using (var sr = new StreamReader(httpPostedFileBase.InputStream))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines.ToArray();
        }
    }
}