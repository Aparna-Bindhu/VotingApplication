using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.Data.Contracts;
using Voting.Data.Model;

namespace Voting.Data.Services
{
    public class StandaloneFileDataRepository : IStandaloneFileDataRepository
    {

        public bool UpdateVotersAndCandidates(int votersId , int candidatesId)
        {
            var votersDetails = JsonConvert.DeserializeObject<List<Voters>>(File.ReadAllText(Constants.VOTERSFILEPATH));
            var voterDataAlreadyExist = votersDetails.Find(x => x.Id == votersId);
            if (voterDataAlreadyExist != null)
            {
                voterDataAlreadyExist.HasVoted = true;

                File.WriteAllText(Constants.VOTERSFILEPATH, JsonConvert.SerializeObject(votersDetails));
            }


            var candidatesDetails = JsonConvert.DeserializeObject<List<Candidates>>(File.ReadAllText(Constants.CANDIDATESFILEPATH));
            var candidateDataAlreadyExist = candidatesDetails.Find(x => x.Id == candidatesId);
            if (candidateDataAlreadyExist != null)
            {
                candidateDataAlreadyExist.Votes += 1;

                File.WriteAllText(Constants.CANDIDATESFILEPATH, JsonConvert.SerializeObject(candidatesDetails));
            }
            return true;
        }
    }
}
