using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileStatistics.Model;

namespace FileStatistics.BusinessLogic
{
    class FileLoader
    {
        public static FileStatistics.Model.File LoadFile(String path)
        {
            var file = new FileInfo(path);

            int count = 0;
            int shortestIndex = 0;
            int sizeShortestIndex = 0;
            string line;
            TextReader reader = new StreamReader(file.FullName);
            while ((line = reader.ReadLine()) != null)
            {
                if(count == 0)
                {
                    sizeShortestIndex = line.Length;
                }
                if(line.Length < sizeShortestIndex)
                {
                    shortestIndex = count;
                    sizeShortestIndex = line.Length;
                }
                count++;
            }
            reader.Close();

            return new Model.File
            {
                Name = file.Name,
                Directory = file.DirectoryName,
                Bytes = file.Length,
                Lines = count,
                ShortestLine = shortestIndex
            };
        }

    }
}
