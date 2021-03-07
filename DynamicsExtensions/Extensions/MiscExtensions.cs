using System.Collections.Generic;
using System.IO;

namespace DynamicsExtensions.Extensions
{
    public static class MiscExtensions
    {
        public static IEnumerable<string> ReadAllLines(this Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}