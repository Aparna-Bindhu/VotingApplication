using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Voting.Domain.Contracts;
using Voting.Domain.Model;
using WebAPI.Controllers;

namespace Voting.WebAPI.Tests
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
        public void Save_With_Null_ThrowArgumentNullException()
        {
            //Arrange
            _mockVotersHandler.Setup(x => x.GetAllDetails()).Returns(GetMockedDetailsOfVoters());

            // Act 
            var result = _votersController.GetAllVoters();

            //Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
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

        //[TestMethod]
        //public void CheckForDuplicateArticles_Null_ThrowArgumentNullException()
        //{
        //    // Act 
        //    var result = _articleContentController.CheckForDuplicateArticles(null);

        //    //Arrange
        //    var error = getErrorValue((result as dynamic).Exception.InnerException.Message);
        //    Assert.AreEqual("Article cannot be null", error);
        //}

        //[TestMethod]
        //public void CheckForDuplicateArticles_SubmissionId_Zero_ThrowArgumentNullException()
        //{
        //    // Act 
        //    var result = _articleContentController.CheckForDuplicateArticles(GetDuplicateArticlesSubmissionIdZero());

        //    //Arrange
        //    var error = getErrorValue((result as dynamic).Exception.InnerException.Message);
        //    Assert.AreEqual("SubmissionId must be greater than zero", error);
        //}

        //[TestMethod]
        //public void CheckForDuplicateArticles_Switch_False_ThrowArgumentNullException()
        //{
        //    _mockSubmissionApiDefaultConfiguration.Setup(x => x.IsArticleDuplicateCheckEnabled).Returns(false);

        //    // Act 
        //    var result = _articleContentController.CheckForDuplicateArticles(GetDuplicateArticles());

        //    //Arrange
        //    Assert.AreEqual(HttpStatusCode.OK, (result as dynamic).Result.Content);
        //}

        //[TestMethod]
        //public void CheckForDuplicateArticles_Switch_True_ThrowArgumentNullException()
        //{
        //    _mockSubmissionApiDefaultConfiguration.Setup(x => x.IsArticleDuplicateCheckEnabled).Returns(true);

        //    // Act 
        //    var result = _articleContentController.CheckForDuplicateArticles(GetDuplicateArticles());

        //    //Arrange
        //    Assert.AreEqual(null, (result as dynamic).Result.Content);

    }
        #endregion

        #region PRIVATE METHODS
        //private string getErrorValue(dynamic message)
        //{
        //    var error = message.Substring(0, message.IndexOf("\r\n"));
        //    return error;
        //}

        //private DuplicateManuscriptModel GetDuplicateArticlesSubmissionIdZero()
        //{
        //    return new DuplicateManuscriptModel
        //    {
        //        SubmissionId = 0
        //    };
        //}

        //private DuplicateManuscriptModel GetDuplicateArticles()
        //{
        //    return new DuplicateManuscriptModel
        //    {
        //        SubmissionId = 123,
        //        Abstract = "abcd",
        //        BodyText = "absc",
        //        Title = "abcd",
        //        IsValidFormTypeCode = true
        //    };
        //}
        #endregion
    }