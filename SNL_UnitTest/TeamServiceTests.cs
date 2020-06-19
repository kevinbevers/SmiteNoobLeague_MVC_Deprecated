using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SNL_FactoryLayer;
using SNL_InterfaceLayer.DateTransferObjects;
using SNL_InterfaceLayer.Interfaces;
using SNL_LogicLayer.ServiceInterfaces;
using System.Collections.Generic;
using SNL_LogicLayer.Services;
using System.Linq;

namespace SNL_UnitTest
{
    [TestClass]
    public class TeamServiceTests
    {
        //this should not be done. unit test should not depend on a database connection. it is better to have a mocked database.
        //private readonly LogicFactory _logicFactory;
        private readonly ITeamService _teamService;
        public TeamServiceTests()
        {
            //_logicFactory = new LogicFactory(new SNL_PersistenceLayer.ConnectionContext("connectionstringhere"));
            Mock<ITeamContext> mockTeamContext = CreateMockTeamContext();
            Mock<IPlayerContext> mockPlayerContext = CreateMockPlayerContext();
            Mock<IRoleContext> mockRoleContext = CreateMockRoleContext();
            Mock<IDivisionContext> mockDivisionContext = CreateMockDivisionContext();

            ITeamService teamService = new TeamService(mockTeamContext.Object,mockPlayerContext.Object,mockRoleContext.Object,mockDivisionContext.Object);
            _teamService = teamService;
        }
        #region MockSetupMethods
        private static Mock<ITeamContext> CreateMockTeamContext()
        {
            var mockTeamContext = new Mock<ITeamContext>();

            mockTeamContext.Setup(r => r.GetAll()).Returns(
                new List<TeamDTO>
                {
                    new TeamDTO
                    {
                        TeamID = 1,
                        TeamName = "testteam",
                        TeamCaptainID = 1,
                        TeamMember2ID = 2,
                        TeamMember3ID = 3,
                        TeamMember4ID = 4,
                        TeamMember5ID = 5,
                        TeamDivisionID = 1,
                    }
                }.AsEnumerable()
                );
            mockTeamContext.Setup(r => r.GetByID(1)).Returns(
                    new TeamDTO
                    {
                        TeamID = 1,
                        TeamName = "testteam",
                        TeamCaptainID = 1,
                        TeamMember2ID = 2,
                        TeamMember3ID = 3,
                        TeamMember4ID = 4,
                        TeamMember5ID = 5,
                        TeamDivisionID = 1,
                    }
                );
            return mockTeamContext;
        }
        private static Mock<IPlayerContext> CreateMockPlayerContext()
        {
            var mockContext = new Mock<IPlayerContext>();

            var playerList = new List<PlayerDTO> { 
                new PlayerDTO {
                        PlayerID = 1,
                        PlayerName = "testplayer2",
                        PlayerPlatformID = 9,
                        PlayerRoleID = 1,
                        PlayerTeamID = 1 },
                new PlayerDTO{
                        PlayerID = 2,
                        PlayerName = "testplayer2",
                        PlayerPlatformID = 9,
                        PlayerRoleID = 2,
                        PlayerTeamID = 1},
                new PlayerDTO{
                        PlayerID = 3,
                        PlayerName = "testplayer3",
                        PlayerPlatformID = 9,
                        PlayerRoleID = 3,
                        PlayerTeamID = 1},
                new PlayerDTO{
                PlayerID = 4,
                PlayerName = "testplayer4",
                PlayerPlatformID = 9,
                PlayerRoleID = 4,
                PlayerTeamID = 1},
                new PlayerDTO{
                PlayerID = 5,
                PlayerName = "testplayer5",
                PlayerPlatformID = 9,
                PlayerRoleID = 5,
                PlayerTeamID = 1 }

            };

            mockContext.Setup(r => r.GetAll()).Returns(playerList.AsEnumerable());
            mockContext.Setup(r => r.GetByID(1)).Returns(playerList.Where(p => p.PlayerID == 1).FirstOrDefault());
            mockContext.Setup(r => r.GetByID(2)).Returns(playerList.Where(p => p.PlayerID == 2).FirstOrDefault());
            mockContext.Setup(r => r.GetByID(3)).Returns(playerList.Where(p => p.PlayerID == 3).FirstOrDefault());
            mockContext.Setup(r => r.GetByID(4)).Returns(playerList.Where(p => p.PlayerID == 4).FirstOrDefault());
            mockContext.Setup(r => r.GetByID(5)).Returns(playerList.Where(p => p.PlayerID == 5).FirstOrDefault());


            return mockContext;
        }
        private static Mock<IRoleContext> CreateMockRoleContext()
        {
            var mockContext = new Mock<IRoleContext>();

            var rolelist = new List<RoleDTO> {
                new RoleDTO {
                            RoleID = 1,
                            RoleName = "Sole",
                            RoleDescription = "Plays mostly warriors and occupies the shortlane"},
                new RoleDTO{
                            RoleID = 1,
                            RoleName = "Jungle",
                            RoleDescription = "Plays mostly assassins and occupies the jungle, doesn't really farm a lane"},
                new RoleDTO{
                            RoleID = 1,
                            RoleName = "Mid",
                            RoleDescription = "Plays mostly mages and occupies the midlane"},
                new RoleDTO{
                            RoleID = 1,
                            RoleName = "Support",
                            RoleDescription = "Plays mostly guardians and occupies the longlane for a shortwhile, then roams the map"},
                new RoleDTO{
                            RoleID = 1,
                            RoleName = "ADC",
                            RoleDescription = "Plays mostly Hunters and occupies the longlane"},

            };

            mockContext.Setup(r => r.GetAll()).Returns(rolelist.AsEnumerable());
            mockContext.Setup(r => r.GetByID(1)).Returns(rolelist.Where(p => p.RoleID == 1).FirstOrDefault());
            mockContext.Setup(r => r.GetByID(2)).Returns(rolelist.Where(p => p.RoleID == 2).FirstOrDefault());
            mockContext.Setup(r => r.GetByID(3)).Returns(rolelist.Where(p => p.RoleID == 3).FirstOrDefault());
            mockContext.Setup(r => r.GetByID(4)).Returns(rolelist.Where(p => p.RoleID == 4).FirstOrDefault());
            mockContext.Setup(r => r.GetByID(5)).Returns(rolelist.Where(p => p.RoleID == 5).FirstOrDefault());


            return mockContext;
        }
        private static Mock<IDivisionContext> CreateMockDivisionContext()
        {
            var mockContext = new Mock<IDivisionContext>();

            var divisionlist = new List<DivisionDTO> {
                new DivisionDTO {
                            DivisionID = 1,
                            DivisionName = "Division1",
                            DivisionDescription = "This is the division with all the best players",
                            DivisionTeamIDs = new List<int?> {1}, }

            };

            mockContext.Setup(r => r.GetAll()).Returns(divisionlist.AsEnumerable());
            mockContext.Setup(r => r.GetByID(1)).Returns(divisionlist.Where(p => p.DivisionID == 1).FirstOrDefault());


            return mockContext;
        }
        #endregion
        [TestMethod]
        public void GetTeamByIDTest()
        {
            //ACT
            var team1 = _teamService.GetByID(1);
            //ASSERT
            Assert.IsTrue(team1.TeamID == 1);
        }
        [TestMethod]
        public void TeamHasMembersTest()
        {
            //ACT
            var team1 = _teamService.GetByID(1);
            //ASSERT
            Assert.IsTrue(team1.TeamMembers.Count > 0);
            Assert.IsTrue(team1.TeamMembers.Where(m => m.PlayerName != null || m.PlayerName != "").Count() > 0);
        }
        [TestMethod]
        public void TeamIsInaDivisionTest()
        {
            //ACT
            var team1 = _teamService.GetByID(1);
            //ASSERT
            Assert.IsNotNull(team1.TeamDivision);
        }
        [TestMethod]
        public void TeamHasACaptainTest()
        {
            //ACT
            var team1 = _teamService.GetByID(1);
            //ASSERT
            Assert.IsNotNull(team1.TeamCaptain);
        }
    }
}
