using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Voting.Domain.Contracts;
using Voting.Domain.Model;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/voters")]
    public class VotersController : ApiController
    {
        private readonly IEntityService<VotersDetails> _votersHandler;
        private readonly IVotersStandaloneServices _standaloneServices;

        public VotersController(IEntityService<VotersDetails> votersHandler, IVotersStandaloneServices standaloneServices)
        {
            _votersHandler = votersHandler ?? throw new ArgumentNullException(nameof(votersHandler));
            _standaloneServices = standaloneServices ?? throw new ArgumentNullException(nameof(standaloneServices));
        }

        [HttpGet]
        [Route("get")]
        public IHttpActionResult GetAllVoters()
        {
            try
            {
                List<VotersDetails> votersDetails = _votersHandler.GetAllDetails();
                List<Voters> votersList = MapGetAllVotersList(votersDetails);
                return Ok(votersList);
            }
            catch (Exception ex)
            {
                // Log the exception
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("unvotedvoters")]
        public IHttpActionResult GetUnvotedVotersList()
        {
            try
            {
                List<VotersDetails> votersDetails = _standaloneServices.GetUnvotedVotersList();
                List<Voters> votersList = MapGetAllVotersList(votersDetails);
                return Ok(votersList);
            }
            catch (Exception ex)
            {
                // Log the exception
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("addvoter")]
        public IHttpActionResult AddVoters([FromBody] Voters votersModel)
        {
            try
            {
                if (votersModel != null)
                {
                    VotersDetails votersdetails = MapVotersModelToVotersDetailsCommand(votersModel);
                    var value = _votersHandler.Add(votersdetails);
                    return Ok(value);
                }
                return null;
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        private List<Voters> MapGetAllVotersList(List<VotersDetails> votersDetails)
        {
            return votersDetails.Select(voters => new Voters
            {
                Id = voters.Id,
                Name = voters.Name,
                HasVoted = voters.HasVoted
            }).ToList();
        }
        private VotersDetails MapVotersModelToVotersDetailsCommand(Voters votersModel)
        {
            return new VotersDetails()
            {
                Id = votersModel.Id,
                Name = votersModel.Name,
                HasVoted = votersModel.HasVoted
            };
        }
    }
}