using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyzerManager
{
    public interface ITool
    {
        int Id { get; set; }
        string Description { get; set; }
        float? Version { get; set; }
        bool IsDeprecated { get; set; }
        string CreatorName { get; set; }
        string CreatorEmail { get; set; }
        Type[] ImplementedInterfaces { get; }

    }
}
