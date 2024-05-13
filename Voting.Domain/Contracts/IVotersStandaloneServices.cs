using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.Domain.Model;

namespace Voting.Domain.Contracts
{
    public interface IVotersStandaloneServices
    {
        List<VotersDetails> GetUnvotedVotersList();
    }
}
