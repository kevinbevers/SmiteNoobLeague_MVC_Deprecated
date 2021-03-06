﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using SNL_LogicLayer;
using SNL_InterfaceLayer.DateTransferObjects;
using SNL_LogicLayer.Models;
using SNL_InterfaceLayer.Interfaces;
using SNL_LogicLayer.ServiceInterfaces;

namespace SNL_LogicLayer.Services
{
    public class TeamService : ITeamService
    {
        //contexts
        private readonly ITeamContext _teamContext;
        private readonly IPlayerContext _playerContext;
        private readonly IRoleContext _roleContext;
        private readonly IDivisionContext _divisionContext;

        public TeamService(ITeamContext teamContext, IPlayerContext playerContext, IRoleContext roleContext, IDivisionContext divisionContext)
        {
            _teamContext = teamContext;
            _playerContext = playerContext;
            _roleContext = roleContext;
            _divisionContext = divisionContext;
        }

        //CRUD
        public void Add(Team entity)
        {
                //validation for team members already being in a team happens in the view.
                TeamDTO tDTO = new TeamDTO
                {
                    TeamID = entity.TeamID,
                    TeamName = entity.TeamName,
                    TeamCaptainID = entity.TeamCaptain?.PlayerID, //null propagation
                    TeamDivisionID = entity.TeamDivision?.DivisionID, // null propagation
                    TeamLogo = entity.TeamLogo,
                    TeamMember2ID = entity.TeamMembers?.Count > 0 ? entity.TeamMembers[0].PlayerID : null,
                    TeamMember3ID = entity.TeamMembers?.Count > 1 ? entity.TeamMembers[1].PlayerID : null,
                    TeamMember4ID = entity.TeamMembers?.Count > 2 ? entity.TeamMembers[2].PlayerID : null,
                    TeamMember5ID = entity.TeamMembers?.Count > 3 ? entity.TeamMembers[3].PlayerID : null,
                };
            //set / get the teamID to use in the other contexts
            entity.TeamID = _teamContext.Add(tDTO);
            
            //add players to DB
            if (entity.TeamMembers != null)
            {
                foreach (var player in entity?.TeamMembers)
                {
                    if (player.PlayerID != null)
                    {
                        PlayerDTO p = new PlayerDTO
                        {
                            PlayerID = player.PlayerID,
                            PlayerName = player.PlayerName,
                            PlayerPlatformID = player.PlayerPlatformID,
                            PlayerRoleID = player.PlayerRole?.RoleID,
                            PlayerTeamID = entity.TeamID
                        };
                        //if the player does or does not exist in the current database
                        if (_playerContext.GetByID(p.PlayerID).PlayerID == null) { _playerContext.Add(p); } else { _playerContext.Update(p); }
                    }
                }
            }
                //add captain to DB
                if (entity.TeamCaptain?.PlayerID != null)
                {
                    if (_playerContext.GetByID(entity.TeamCaptain.PlayerID).PlayerID != null)
                    {
                        var captain = _playerContext.GetByID(entity.TeamCaptain.PlayerID);
                        captain.PlayerTeamID = entity.TeamID;
                         _playerContext.Update(captain);
                    }
                }
        }
        public IEnumerable<Team> GetAll()
        {
            IEnumerable<TeamDTO> tDTOList = _teamContext.GetAll();
            List<Team> teamList = new List<Team>();

            foreach(var tDTO in tDTOList)
            {
                Team t = BuildTeamModel(tDTO);
                teamList.Add(t);
            }
            return teamList;
        }
        public Team GetByID(int? id)
        {
            //team
            TeamDTO tDTO = _teamContext.GetByID(id);

            Team t = BuildTeamModel(tDTO);

            return t;
        }
        public void Remove(Team entity)
        {
            var team = _teamContext.GetByID(entity.TeamID);
            //remove teamID from all players in the team. making them free agents
            var captain = _playerContext.GetByID(team.TeamCaptainID);
            captain.PlayerTeamID = default;
            _playerContext.Update(captain);
            //member 2
            var t2 = _playerContext.GetByID(team.TeamMember2ID);
            t2.PlayerTeamID = null;
            _playerContext.Update(t2);
            //member 3
            var t3 = _playerContext.GetByID(team.TeamMember3ID);
            t3.PlayerTeamID = null;
            _playerContext.Update(t3);
            //member 4
            var t4 = _playerContext.GetByID(team.TeamMember4ID);
            t4.PlayerTeamID = null;
            _playerContext.Update(t4);
            //member 5
            var t5 = _playerContext.GetByID(team.TeamMember5ID);
            t5.PlayerTeamID = null;
            _playerContext.Update(t5);
            //division
            var division = _divisionContext.GetByID(team.TeamDivisionID);
            division.DivisionTeamIDs.ToList().Remove(team.TeamID);
            _divisionContext.Update(division);
            //only need primary key id to remove
            TeamDTO tDTO = new TeamDTO
            {
                TeamID = entity.TeamID,
            };

            _teamContext.Remove(tDTO);
        }
        public void Update(Team entity)
        {
            var oldValues = _teamContext.GetByID(entity.TeamID);
            List<int?> currentPlayers = new List<int?> {oldValues.TeamMember2ID, oldValues.TeamMember3ID, oldValues.TeamMember4ID, oldValues.TeamMember5ID };

            TeamDTO tDTO = new TeamDTO
            {
                TeamID = entity.TeamID, //normal values are nullable
                TeamName = entity.TeamName,
                TeamCaptainID = entity.TeamCaptain?.PlayerID, //null propagation because this comes from an object
                TeamDivisionID = oldValues.TeamDivisionID, // null propagation teamdivision doesn't get edited in manage teams
                TeamLogo = entity.TeamLogo,
                TeamMember2ID = entity.TeamMembers?.Count > 0 ? entity.TeamMembers[0].PlayerID : null,
                TeamMember3ID = entity.TeamMembers?.Count > 1 ? entity.TeamMembers[1].PlayerID : null,
                TeamMember4ID = entity.TeamMembers?.Count > 2 ? entity.TeamMembers[2].PlayerID : null,
                TeamMember5ID = entity.TeamMembers?.Count > 3 ? entity.TeamMembers[3].PlayerID : null,
            };
            _teamContext.Update(tDTO);
            //add players to DB
            if (entity?.TeamMembers != null)
            {
                foreach (var player in entity?.TeamMembers)
                {
                    if (player.PlayerID != null)
                    {
                        if (currentPlayers.Contains(player.PlayerID))
                        {
                            PlayerDTO p = new PlayerDTO
                            {
                                PlayerID = player.PlayerID,
                                PlayerName = player.PlayerName,
                                PlayerPlatformID = player.PlayerPlatformID,
                                PlayerRoleID = player.PlayerRole?.RoleID,
                                PlayerTeamID = entity.TeamID
                            };
                            //if the player does or does not exist in the current database
                            if (_playerContext.GetByID(p.PlayerID).PlayerID == null) { _playerContext.Add(p); } else { _playerContext.Update(p); }
                        }
                        else
                        {
                            var pID = currentPlayers.Where(p => p.Value == player.PlayerID).FirstOrDefault();

                            var pToUpdate = _playerContext.GetByID(pID);
                            pToUpdate.PlayerTeamID = null;
                            _playerContext.Update(pToUpdate);
                        }
                    }
                }
            }
            //add captain to DB //captain always exists in the database because it is required when an account is made 
            if (entity.TeamCaptain?.PlayerID != null)
            {
                if (oldValues.TeamCaptainID == entity.TeamCaptain.PlayerID)
                {
                    var captainValues = _playerContext.GetByID(oldValues.TeamCaptainID);

                    PlayerDTO captain = new PlayerDTO
                    {
                        PlayerID = entity.TeamCaptain.PlayerID,
                        PlayerName = captainValues.PlayerName,
                        PlayerPlatformID = captainValues.PlayerPlatformID,
                        PlayerRoleID = entity.TeamCaptain.PlayerRole?.RoleID,
                        PlayerTeamID = entity.TeamID
                    };
                    _playerContext.Update(captain);
                }
                else
                {
                    //remove team from the previous captain
                    var pToUpdate = _playerContext.GetByID(oldValues.TeamCaptainID);
                    pToUpdate.PlayerTeamID = null;
                    _playerContext.Update(pToUpdate);
                }
            }
        }
        //Extra business logic
        public bool TeamNameAvailable(string teamname)
        {  
            return _teamContext.NameAvailable(teamname);
        }
        public bool CaptainAvailable(int captainid)
        {
            return _teamContext.CaptainAvailable(captainid);
        }
        public bool PlayerAvailable(int playerid)
        {
            return _teamContext.PlayerAvailable(playerid);
        }

        //move to playerService later
        public bool IsPlayerPickable(Player player, int? teamID)
        {
            if(_playerContext.GetByID(player.PlayerID)?.PlayerTeamID != null)
            {
                if (_playerContext.GetByID(player.PlayerID)?.PlayerTeamID == teamID)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        #region PrivateMethods
        private Team BuildTeamModel(TeamDTO tDTO)
        {
            //team members
            PlayerDTO CaptainDTO = _playerContext.GetByID(tDTO.TeamCaptainID) ?? new PlayerDTO();
            PlayerDTO tm2DTO = _playerContext.GetByID(tDTO.TeamMember2ID) ?? new PlayerDTO();
            PlayerDTO tm3DTO = _playerContext.GetByID(tDTO.TeamMember3ID) ?? new PlayerDTO();
            PlayerDTO tm4DTO = _playerContext.GetByID(tDTO.TeamMember4ID) ?? new PlayerDTO();
            PlayerDTO tm5DTO = _playerContext.GetByID(tDTO.TeamMember5ID) ?? new PlayerDTO();
            //get player role with the role context
            var roles = _roleContext.GetAll();
            var cRole = roles.Where(i => i.RoleID == CaptainDTO.PlayerRoleID).FirstOrDefault() ?? new RoleDTO();
            var t2Role = roles.Where(i => i.RoleID == tm2DTO.PlayerRoleID).FirstOrDefault() ?? new RoleDTO();
            var t3Role = roles.Where(i => i.RoleID == tm3DTO.PlayerRoleID).FirstOrDefault() ?? new RoleDTO();
            var t4Role = roles.Where(i => i.RoleID == tm4DTO.PlayerRoleID).FirstOrDefault() ?? new RoleDTO();
            var t5Role = roles.Where(i => i.RoleID == tm5DTO.PlayerRoleID).FirstOrDefault() ?? new RoleDTO();
            //create list of team members excluding the captain
            List<Player> teamMembers = new List<Player>
            {
                new Player { PlayerID = tm2DTO.PlayerID, PlayerName = tm2DTO.PlayerName, PlayerPlatformID = tm2DTO.PlayerPlatformID, PlayerRole = new Role {RoleID = t2Role.RoleID, RoleName = t2Role.RoleName, RoleDescription = t2Role.RoleDescription } },
                new Player {  PlayerID = tm3DTO.PlayerID, PlayerName = tm3DTO.PlayerName, PlayerPlatformID = tm3DTO.PlayerPlatformID, PlayerRole = new Role {RoleID = t3Role.RoleID, RoleName = t3Role.RoleName, RoleDescription = t3Role.RoleDescription }},
                new Player {  PlayerID = tm4DTO.PlayerID, PlayerName = tm4DTO.PlayerName, PlayerPlatformID = tm4DTO.PlayerPlatformID, PlayerRole = new Role {RoleID = t4Role.RoleID, RoleName = t4Role.RoleName, RoleDescription = t4Role.RoleDescription }},
                new Player {  PlayerID = tm5DTO.PlayerID, PlayerName = tm5DTO.PlayerName, PlayerPlatformID = tm5DTO.PlayerPlatformID, PlayerRole = new Role {RoleID = t5Role.RoleID, RoleName = t5Role.RoleName, RoleDescription = t5Role.RoleDescription }},

            };
            //TeamDivision
            DivisionDTO div = _divisionContext.GetByID(tDTO.TeamDivisionID) ?? new DivisionDTO(); //could do something with the division


            Team t = new Team
            {
                TeamID = tDTO.TeamID,
                TeamName = tDTO.TeamName,
                TeamLogo = tDTO.TeamLogo,
                TeamCaptain = new Player { PlayerID = CaptainDTO.PlayerID, PlayerName = CaptainDTO.PlayerName, PlayerPlatformID = CaptainDTO.PlayerPlatformID, PlayerRole = new Role { RoleID = cRole.RoleID, RoleName = cRole.RoleName, RoleDescription = cRole.RoleDescription } },
                TeamMembers = teamMembers,
                TeamDivision = new Division { DivisionID = div.DivisionID, DivisionName = div.DivisionName },
            };
            return t;
        }
        #endregion
    }
}
