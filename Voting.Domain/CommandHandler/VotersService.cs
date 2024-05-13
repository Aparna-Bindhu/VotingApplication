using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.Data.Contracts;
using Voting.Data.Model;
using Voting.Domain.Contracts;
using Voting.Domain.Model;

namespace Voting.Domain.CommandHandler
{
    public class VotersService : IEntityService<VotersDetails>, IVotersStandaloneServices
    {

        private readonly IJsonFileDataRepository<Voters> _votersFileData;
        public VotersService(IJsonFileDataRepository<Voters> votersFileData)
        {
            _votersFileData = votersFileData ?? throw new ArgumentNullException(nameof(votersFileData));
        }

        public List<VotersDetails> GetAllDetails()
        {
            var voters = MapVotersDetails(_votersFileData.GetAll());
            return voters != null ? voters.ToList() : new List<VotersDetails>();
        }

        public List<VotersDetails> Add(VotersDetails voters)
        {
            var votersList = new List<VotersDetails>();

                var votersDetailsFetched = _votersFileData.GetAll();
                voters.Id = votersDetailsFetched.Count + 1;
                votersDetailsFetched.Add(MapVotersDetailsToAddVoter(voters));
                var list = _votersFileData.Add(votersDetailsFetched, voters.Id);
                votersList = MapVotersDetails(list);
            return votersList != null ? votersList.ToList() : new List<VotersDetails>();
        }

        public List<VotersDetails> GetUnvotedVotersList()
        {
            var unvotedVotersList = GetAllDetails()?.Where(x => !x.HasVoted);
            return unvotedVotersList != null ? unvotedVotersList.ToList() : new List<VotersDetails>();
        }

        private Voters MapVotersDetailsToAddVoter(VotersDetails voter)
        {
            return new Voters
            {
                Id = voter.Id,
                Name = voter.Name,
                HasVoted = voter.HasVoted
            };
        }

        private List<VotersDetails> MapVotersDetails(List<Voters> voters)
        {
            return voters.Select(voter => new VotersDetails
            {
                Id = voter.Id,
                Name = voter.Name,
                HasVoted = voter.HasVoted
            }).ToList();
        }

    }
}
