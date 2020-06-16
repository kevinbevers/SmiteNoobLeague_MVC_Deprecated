using Microsoft.VisualStudio.TestTools.UnitTesting;
using SNL_LogicLayer;

namespace SNL_UnitTest
{
    [TestClass]
    public class TeamServiceTests
    {
        private readonly LogicFactory _logicFactory;

        //this should not be done. unit test should not depend on a database connection it is better to have a mocked database.
        public TeamServiceTests()
        {
            _logicFactory = new LogicFactory(new SNL_PersistenceLayer.ConnectionContext("connectionstringhere"));
        }

        [TestMethod]
        public void GetTeamByID()
        {
            var teamService = _logicFactory.GetTeamService();
            var team1 = teamService.GetByID(1);

            Assert.IsTrue(team1.TeamID == 1);
        }
    }
}
