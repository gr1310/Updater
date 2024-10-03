using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalyzerManager;

namespace DataExchangeModule
{
    public interface IDllLoader
    {
        public Dictionary<string, List<string>> LoadToolsFromFolder(string folderPath);

        public string ResolveDllNamingConflict(string dllName);
    }

}
