using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Voting.Data.Contracts;
using Voting.Data.Model;

namespace Voting.Data.Services
{
    public class CandidatesFileData : IJsonFileDataRepository<Candidates>
    {
        public List<Candidates> GetAll()
        {
            try
            {
                var CandidatesDetails = JsonConvert.DeserializeObject<List<Candidates>>(File.ReadAllText(Constants.CANDIDATESFILEPATH));
                return CandidatesDetails ?? new List<Candidates>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Candidates> Add(List<Candidates> Candidates, int id)
        {
            File.WriteAllText(Constants.CANDIDATESFILEPATH, JsonConvert.SerializeObject(Candidates));

            var updatedCandidates = GetAll();

            // Check if the voter with the specified ID was successfully added
            var addedVoter = updatedCandidates.Find(v => v.Id == id);
            if (addedVoter != null)
            {
                // If the voter was successfully added, set success flag and data ID
                return updatedCandidates;
            }
            return new List<Candidates>();
        }

    }
}
