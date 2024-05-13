using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Voting.Domain.Contracts;
using Voting.Domain.Model;
using WebAPI.Controllers;

namespace Voting.Web.API.Test
{
    [TestClass]
    public class VotersControllerTest
    {

        #region PRIVATE PROPERTIES

        private Mock<IEntityService<VotersDetails>> _mockVotersHandler;
        private Mock<IVotersStandaloneServices> _mockStandaloneServices;
        private VotersController _votersController;

        #endregion
        #region TEST INITIALIZATION

        [TestInitialize]
        public void InitializeTest()
        {
            _mockVotersHandler = new Mock<IEntityService<VotersDetails>>();
            _mockStandaloneServices = new Mock<IVotersStandaloneServices>();
            _votersController = new VotersController(_mockVotersHandler.Object, _mockStandaloneServices.Object);
        }
        #endregion

        #region UNIT TEST METHODS
        [TestMethod]
        public void GetAllVotersDetails_without_error()
        {
            //Arrange
            _mockVotersHandler.Setup(x => x.GetAllDetails()).Returns(GetMockedDetailsOfVoters());

            // Act 
            var result = _votersController.GetAllVoters();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetUnVotedVotersDetails_without_error()
        {
            //Arrange
            _mockStandaloneServices.Setup(x => x.GetUnvotedVotersList()).Returns(GetMockedDetailsOfUnVotedVoters());

            // Act 
            var result = _votersController.GetAllVoters();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AddVoterDetails_without_error()
        {
            //Arrange
            _mockVotersHandler.Setup(x => x.Add(It.IsAny<VotersDetails>())).Returns(GetMockedDetailsOfVoters());

            // Act 
            var result = _votersController.AddVoters(new WebAPI.Models.Voters() {
                Id = 2,
                Name = "abcdef",
                HasVoted = false
            });

            //Assert
            Assert.IsNotNull(result);
        }
        #endregion

        private List<VotersDetails> GetMockedDetailsOfUnVotedVoters()
        {
            return new List<VotersDetails>
            {
                new VotersDetails
                {
                    Id = 1,
                    Name = "abcd",
                    HasVoted = false
                }
            };
        }

        private List<VotersDetails> GetMockedDetailsOfVoters()
        {
            return new List<VotersDetails>
            {
                new VotersDetails
                {
                    Id = 1,
                    Name = "abcd",
                    HasVoted = false
                },
                new VotersDetails
                {
                    Id = 2,
                    Name = "abcdef",
                    HasVoted = true
                }
            };
        }
    }
}
