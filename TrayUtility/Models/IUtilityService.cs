using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesHandler
{
    public interface IUtilityService
    {
        IEnumerable<Utility> GetUtilities();
        void SaveDisabledUtilitiesNameToFile(IEnumerable<Utility> utilities);
        IEnumerable<Utility> ReadDisabledUtilitiesNameFromFile(List<Utility> utilities);
    }
}
