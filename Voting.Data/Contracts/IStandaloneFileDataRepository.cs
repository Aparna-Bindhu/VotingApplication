using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.Data.Contracts
{
    public interface IStandaloneFileDataRepository
    {
        bool UpdateVotersAndCandidates(int votersId , int candidatesId);
    }
}
