using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutSuranceTest.Interface
{
    public interface IWordCounter
    {
        IDictionary<string, int> CountWords(string path);
    }
}
