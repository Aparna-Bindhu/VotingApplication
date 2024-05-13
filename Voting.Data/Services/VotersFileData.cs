using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Voting.Data.Contracts;
using Voting.Data.Model;

namespace Voting.Data.Services
{
    public class VotersFileData : IJsonFileDataRepository<Voters>
    {
        public List<Voters> GetAll()
        {
            try
            {
                var votersDetails = JsonConvert.DeserializeObject<List<Voters>>(File.ReadAllText(Constants.VOTERSFILEPATH));
                return votersDetails ?? new List<Voters>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Voters> Add(List<Voters> voters, int id)
        {
            File.WriteAllText(Constants.VOTERSFILEPATH, JsonConvert.SerializeObject(voters));

            var updatedVoters = GetAll();

            // Check if the voter with the specified ID was successfully added
            var addedVoter = updatedVoters.Find(v => v.Id == id);
            if (addedVoter != null)
            {
                // If the voter was successfully added, set success flag and data ID
                return updatedVoters;
            }
            return new List<Voters>();
        }
    }
}
