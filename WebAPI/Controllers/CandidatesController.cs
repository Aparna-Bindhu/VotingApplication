using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Voting.Domain.CommandHandler;
using Voting.Domain.Contracts;
using Voting.Domain.Model;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/candidates")]
    public class CandidatesController : ApiController
    {
        private readonly IEntityService<CandidatesDetails> _candidatesHandler;
        private readonly ICandidateStandaloneServices _standaloneHandler;

        public CandidatesController(IEntityService<CandidatesDetails> candidatesHandler, ICandidateStandaloneServices standaloneHandler)
        {
            _candidatesHandler = candidatesHandler ?? throw new ArgumentNullException(nameof(candidatesHandler));
            _standaloneHandler = standaloneHandler ?? throw new ArgumentNullException(nameof(standaloneHandler));
        }

        [HttpGet]
        [Route("get")]
        public IHttpActionResult GetAllCandidates()
        {
            try
            {
                List<CandidatesDetails> candidatesDetails = _candidatesHandler.GetAllDetails();
                List<Candidates> candidatesList = MapGetAllCandidatesList(candidatesDetails);
                return Ok(candidatesList);
            }
            catch (Exception ex)
            {
                // Log the exception
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("addcandidate")]
        public IHttpActionResult AddCandidates([FromBody] Candidates candidatesModel)
        {
            try
            {
                if (candidatesModel != null)
                {
                    CandidatesDetails candidatesDetails = MapVotersModelToCandidatesDetailsCommand(candidatesModel);
                    var value = _candidatesHandler.Add(candidatesDetails);
                    return Ok(value);
                }
                return null;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateVotersAndCandidates([FromBody] VotersAndCandidatesModel votersAndCandidatesModel)
        {
            try
            {
                if (votersAndCandidatesModel != null)
                {
                    VotersAndCandidates votersAndCandidates = MapVotersAndCandidatesModelToUpdateDetails(votersAndCandidatesModel);
                    var value = _standaloneHandler.UpdateVotersAndCandidates(votersAndCandidates);
                    return Ok(value);
                }
                return null;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        private VotersAndCandidates MapVotersAndCandidatesModelToUpdateDetails(VotersAndCandidatesModel votersAndCandidates)
        {
            return new VotersAndCandidates
            {
                VotersId = votersAndCandidates.VotersId,
                CandidatesId = votersAndCandidates.CandidatesId
            };
        }

        private List<Candidates> MapGetAllCandidatesList(List<CandidatesDetails> candidatesDetails)
        {
            return candidatesDetails.Select(voters => new Candidates
            {
                Id = voters.Id,
                Name = voters.Name,
                Votes = voters.Votes
            }).ToList();
        }
        private CandidatesDetails MapVotersModelToCandidatesDetailsCommand(Candidates candidateModel)
        {
            return new CandidatesDetails()
            {
                Id = candidateModel.Id,
                Name = candidateModel.Name,
                Votes = candidateModel.Votes
            };
        }
    }
}