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
    public class CandidatesService : IEntityService<CandidatesDetails>, ICandidateStandaloneServices
    {

        private readonly IJsonFileDataRepository<Candidates> _candidatesFileData;
        private readonly IStandaloneFileDataRepository _standaloneFileDataRepository;
        public CandidatesService(IJsonFileDataRepository<Candidates> candidatesFileData, IStandaloneFileDataRepository standaloneFileDataRepository)
        {
            _candidatesFileData = candidatesFileData ?? throw new ArgumentNullException(nameof(candidatesFileData));
            _standaloneFileDataRepository = standaloneFileDataRepository ?? throw new ArgumentNullException(nameof(standaloneFileDataRepository));
        }

        public List<CandidatesDetails> GetAllDetails()
        {
            var Candidates = MapCandidatesDetails(_candidatesFileData.GetAll());
            return Candidates != null ? Candidates.ToList() : new List<CandidatesDetails>();
        }

        public List<CandidatesDetails> Add(CandidatesDetails Candidates)
        {
                var CandidatesDetailsFetched = _candidatesFileData.GetAll();
                Candidates.Id = CandidatesDetailsFetched.Count + 1;
                CandidatesDetailsFetched.Add(MapCandidatesDetailsToAddVoter(Candidates));
                var list = _candidatesFileData.Add(CandidatesDetailsFetched, Candidates.Id);
                var CandidatesList = MapCandidatesDetails(list);
            return CandidatesList != null ? CandidatesList.ToList() : new List<CandidatesDetails>();
        }
        public bool UpdateVotersAndCandidates(VotersAndCandidates votersAndCandidates)
        {
            var value =  _standaloneFileDataRepository.UpdateVotersAndCandidates(votersAndCandidates.VotersId, votersAndCandidates.CandidatesId);
            return value;
        }

        private Candidates MapCandidatesDetailsToAddVoter(CandidatesDetails voter)
        {
            return new Candidates
            {
                Id = voter.Id,
                Name = voter.Name,
                Votes = voter.Votes
            };
        }

        public List<CandidatesDetails> MapCandidatesDetails(List<Candidates> Candidates)
        {
            return Candidates.Select(voter => new CandidatesDetails
            {
                Id = voter.Id,
                Name = voter.Name,
                Votes = voter.Votes
            }).ToList();
        }

    }
}
