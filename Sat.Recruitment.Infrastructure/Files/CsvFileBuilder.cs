using System.Collections.Generic;
using System.IO;
using Sat.Recruitment.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure.Files
{
    public class CsvFileBuilder : ICsvFileBuilder
    {
        //private readonly Mutex mutex = new Mutex();

        private static readonly object mylock = new object();

        public Task<IList<string>> GetDataFile(string filePath)
        {
            IList<string> lines = new List<string>();
            //return Task.Factory.StartNew(() => {
                lock (mylock)
                {
                    using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                    // Do your writing here.
                    using (StreamReader reader = new StreamReader(stream))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                lines.Add(line);
                            }
                        }
                    }
                    return Task.FromResult(lines);
                }
            //});
        }

        public Task<int> WriteFile(string filePath, IEnumerable<string> lines)
        {
            int result = 0;
            //return Task.Factory.StartNew(() => {
                lock (mylock)
                {
                    using (FileStream stream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                    {
                        // Do your writing here.
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            foreach (var line in lines)
                            {
                                writer.WriteLine(line);
                                result++;
                            }
                        }
                    }
                    return Task.FromResult(result);
                }   
            //});
        }
    }
}
