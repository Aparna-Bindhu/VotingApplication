using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;

namespace Voting.WebAPI.Tests
{
    [TestClass]
    public class VotersControllerTest
    {
        #region PRIVATE PROPERTIES

        private Mock<IEntityService<VotersDetails>> _mockVotersHandler;
        private Mock<IVotersStandaloneServices> _mockStandaloneServices;

        #endregion
        #region TEST INITIALIZATION

        [TestInitialize]
        public void InitializeTest()
        {
            _mockCommandDispatcher = new Mock<ICommandDispatcher>();
            _mockLogger = new Mock<ILogger>();
            _mockQueryDispatcher = new Mock<IQueryDispatcher>();
            _mockSubmissionApiDefaultConfiguration = new Mock<ISubmissionApiDefaultConfiguration>();
            _mockSubmissionReadData = new Mock<ISubmissionReadData>();
            _mockhtmlUtilitiesService = new Mock<IHtmlUtilitiesService>();
            _articleContentController = new ArticleContentController(_mockQueryDispatcher.Object,
                _mockCommandDispatcher.Object,
                _mockLogger.Object, _mockSubmissionApiDefaultConfiguration.Object, _mockhtmlUtilitiesService.Object, _mockSubmissionReadData.Object);
        }
        #endregion

        #region UNIT TEST METHODS
        [TestMethod]
        public void Save_With_Null_ThrowArgumentNullException()
        {
            // Act 
            var result = _articleContentController.Save(null);

            //Arrange
            var error = getErrorValue((result as dynamic).Exception.InnerException.Message);
            Assert.AreEqual("Article cannot be null", error);
        }

        [TestMethod]
        public void CheckForDuplicateArticles_Null_ThrowArgumentNullException()
        {
            // Act 
            var result = _articleContentController.CheckForDuplicateArticles(null);

            //Arrange
            var error = getErrorValue((result as dynamic).Exception.InnerException.Message);
            Assert.AreEqual("Article cannot be null", error);
        }

        [TestMethod]
        public void CheckForDuplicateArticles_SubmissionId_Zero_ThrowArgumentNullException()
        {
            // Act 
            var result = _articleContentController.CheckForDuplicateArticles(GetDuplicateArticlesSubmissionIdZero());

            //Arrange
            var error = getErrorValue((result as dynamic).Exception.InnerException.Message);
            Assert.AreEqual("SubmissionId must be greater than zero", error);
        }

        [TestMethod]
        public void CheckForDuplicateArticles_Switch_False_ThrowArgumentNullException()
        {
            _mockSubmissionApiDefaultConfiguration.Setup(x => x.IsArticleDuplicateCheckEnabled).Returns(false);

            // Act 
            var result = _articleContentController.CheckForDuplicateArticles(GetDuplicateArticles());

            //Arrange
            Assert.AreEqual(HttpStatusCode.OK, (result as dynamic).Result.Content);
        }

        [TestMethod]
        public void CheckForDuplicateArticles_Switch_True_ThrowArgumentNullException()
        {
            _mockSubmissionApiDefaultConfiguration.Setup(x => x.IsArticleDuplicateCheckEnabled).Returns(true);

            // Act 
            var result = _articleContentController.CheckForDuplicateArticles(GetDuplicateArticles());

            //Arrange
            Assert.AreEqual(null, (result as dynamic).Result.Content);

        }
        #endregion

        #region PRIVATE METHODS
        private string getErrorValue(dynamic message)
        {
            var error = message.Substring(0, message.IndexOf("\r\n"));
            return error;
        }

        private DuplicateManuscriptModel GetDuplicateArticlesSubmissionIdZero()
        {
            return new DuplicateManuscriptModel
            {
                SubmissionId = 0
            };
        }

        private DuplicateManuscriptModel GetDuplicateArticles()
        {
            return new DuplicateManuscriptModel
            {
                SubmissionId = 123,
                Abstract = "abcd",
                BodyText = "absc",
                Title = "abcd",
                IsValidFormTypeCode = true
            };
        }
        #endregion
    }
}