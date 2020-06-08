using System;
using System.Collections.Generic;
using System.Text;
using SNL_PersistenceLayer.Interfaces;
using SNL_InterfaceLayer.CustomExceptions;
using SNL_InterfaceLayer.DateTransferObjects;
using MySql.Data.MySqlClient;
using System.Linq;

namespace SNL_PersistenceLayer.Contexts
{
    public class ScheduleContext : IContext<ScheduleDTO>
    {
        private readonly ConnectionContext _con;
        public ScheduleContext(ConnectionContext con)
        {
            _con = con;
        }

        //Create
        public void Add(ScheduleDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO god VALUES(ScheduleID = ?ScheduleID, DivisionID = ?DivisionID, ScheduleName = ?ScheduleName)", conn);
                    //values
                    cmd.Parameters.AddWithValue("ScheduleID", entity.ScheduleID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("DivisionID", entity.DivisionID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ScheduleName", entity.ScheduleName ?? (object)DBNull.Value);
                    //execute command
                    int rowsAffected = cmd.ExecuteNonQuery();
                    //should return if a row is affected or not

                    //add multiple scheduleDetails
                    AddScheduleDetails(entity.ScheduleDetailsList, conn);
                }
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Read
        public IEnumerable<ScheduleDTO> GetAll()
        {
            try
            {
                List<ScheduleDTO> scheduleList = new List<ScheduleDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT ScheduleID,DivisionID,ScheduleName FROM schedule", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ScheduleDTO schedule = new ScheduleDTO
                            {
                                ScheduleID = reader[0] as int? ?? default,
                                DivisionID = reader[1] as int? ?? default,
                                ScheduleName = reader[2] as string ?? default,
                            };
                            schedule.ScheduleDetailsList = GetScheduleDetails(schedule.ScheduleID, conn);

                            scheduleList.Add(schedule);
                        }
                    }
                }
                return scheduleList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public ScheduleDTO GetByID(int? id)
        {
            try
            {
                ScheduleDTO schedule = new ScheduleDTO();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    //join statement for scheduleDetails
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT ScheduleID,DivisionID,ScheduleName FROM schedule WHERE ScheduleID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            schedule.ScheduleID = reader[0] as int? ?? default;
                            schedule.DivisionID = reader[1] as int? ?? default;
                            schedule.ScheduleName = reader[2] as string ?? default;
                        }
                    }

                    //get scheduleDetails
                    schedule.ScheduleDetailsList = GetScheduleDetails(id, conn);

                }
                return schedule;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public IEnumerable<ScheduleDTO> GetByDivisionID(int? id)
        {
            try
            {
                List<ScheduleDTO> scheduleList = new List<ScheduleDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    //join statement for scheduleDetails
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT ScheduleID,DivisionID,ScheduleName FROM schedule WHERE DivisionID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ScheduleDTO schedule = new ScheduleDTO
                            {
                                ScheduleID = reader[0] as int? ?? default,
                                DivisionID = reader[1] as int? ?? default,
                                ScheduleName = reader[2] as string ?? default,
                            };
                            //get scheduleDetails
                            schedule.ScheduleDetailsList = GetScheduleDetails(id, conn);
                            scheduleList.Add(schedule);
                        }
                    }

                }
                return scheduleList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Update
        public void Update(ScheduleDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE schedule SET(DivisionID = ?DivisionID, ScheduleName = ?ScheduleName) WHERE ScheduleID = ?id", conn);
                    //where id is
                    cmd.Parameters.AddWithValue("id", entity.ScheduleID);
                    //values
                    cmd.Parameters.AddWithValue("DivisionID", entity.DivisionID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("TeamID", entity.ScheduleName ?? (object)DBNull.Value);
                    //execute command
                    int rowsAffected = cmd.ExecuteNonQuery();
                    //should return if a row is affected or not

                    //update scheduleDetails
                    UpdateScheduleDetails(entity, conn);
                    //get all then compare and delete the ones that are not in the list anymore then update all the rest.
                    //easier to just drop details and then insert new ones?!


                }
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Delete
        public void Remove(ScheduleDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    //delete scheduledetails?? via database foreign key cascade or here in the DAL? or should i keep them so the data isn't lost
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM schedule WHERE ScheduleID = ?id", conn);
                    //where id =
                    cmd.Parameters.AddWithValue("id", entity.ScheduleID);
                    //execute command
                    int rowsAffected = cmd.ExecuteNonQuery();
                    //should return if a row is affected or not
                }
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }

        private List<ScheduleDetailDTO> GetScheduleDetails(int? id, MySqlConnection conn)
        {
            MySqlCommand detailCmd = new MySqlCommand("SELECT ScheduleDetailsID,ScheduleID,HomeTeamID,AwayTeamID,WeekNumber,MatchNumber FROM scheduledetails WHERE ScheduleID = ?id", conn);
            detailCmd.Parameters.AddWithValue("id", id);
            using (var reader = detailCmd.ExecuteReader())
            {
                List<ScheduleDetailDTO> detailList = new List<ScheduleDetailDTO>();

                while (reader.Read())
                {
                    detailList.Add(new ScheduleDetailDTO
                    {
                        ScheduleDetailsID = reader[0] as int? ?? default,
                        ScheduleID = reader[1] as int? ?? default,
                        HomeTeamID = reader[2] as int? ?? default,
                        AwayTeamID = reader[3] as int? ?? default,
                        WeekNumber = reader[4] as int? ?? default,
                        MatchNumber = reader[5] as int? ?? default,
                    });
                }
                return detailList;
            }
        }
        private void AddScheduleDetails(IEnumerable<ScheduleDetailDTO> scheduleDetailsList, MySqlConnection conn)
        {
            //build a string with all the values in it. no mysql.escapestring needed because integers cannot be used for SQL injection
            StringBuilder sCommand = new StringBuilder("INSERT INTO scheduledetails (ScheduleID, HomeTeamID,AwayTeamID,MatchNumber,WeekNumber) VALUES ");

            List<string> Rows = new List<string>();
            foreach (var detail in scheduleDetailsList)
            {
                Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}')", detail.ScheduleID, detail.HomeTeamID, detail.AwayTeamID, detail.MatchNumber, detail.WeekNumber));
            }
            sCommand.Append(string.Join(",", Rows));
            sCommand.Append(";");

            MySqlCommand addTeamsToDivisionCmd = new MySqlCommand(sCommand.ToString(), conn);
            addTeamsToDivisionCmd.ExecuteNonQuery();
        }
        private void UpdateScheduleDetails(ScheduleDTO entity, MySqlConnection conn)
        {
            List<ScheduleDetailDTO> currentDetails = GetScheduleDetails(entity.ScheduleID, conn);
            List<ScheduleDetailDTO> UpdatedDetails = entity.ScheduleDetailsList.ToList();
            List<ScheduleDetailDTO> DeleteList = new List<ScheduleDetailDTO>();
            List<ScheduleDetailDTO> UpdateList = new List<ScheduleDetailDTO>();

            foreach (var PossibleDelete in currentDetails)
            {
                if (UpdatedDetails.Where(item => item.ScheduleDetailsID == PossibleDelete.ScheduleDetailsID).Count() > 0)
                {
                    UpdateList.Add(PossibleDelete);
                }
                else
                {
                    DeleteList.Add(PossibleDelete);
                }
            }

            MySqlCommand deleteDetailCmd = new MySqlCommand("DELETE FROM scheduledetails WHERE ScheduleDetailsID IN (?ids)", conn);
            //where id is
            string IdList = String.Join(',', DeleteList.Select(item => item.ScheduleDetailsID));
            deleteDetailCmd.Parameters.AddWithValue("ids", MySqlHelper.EscapeString(IdList));

            foreach (var ScheduleDetail in UpdateList)
            {
                if (currentDetails.Where(e => e.ScheduleDetailsID == ScheduleDetail.ScheduleDetailsID).Count() == 1)
                {
                    //update command
                    MySqlCommand updateDetailCmd = new MySqlCommand("UPDATE scheduledetails SET(HomeTeamID = ?HomeTeamID, AwayTeamID = ?AwayTeamID, WeekNumber = ?WeekNumber, MatchNumber = ?MatchNumber) WHERE ScheduleDetailsID = ?id", conn);
                    //where id is
                    updateDetailCmd.Parameters.AddWithValue("id", ScheduleDetail.ScheduleDetailsID);
                    //values
                    updateDetailCmd.Parameters.AddWithValue("HomeTeamID", ScheduleDetail.HomeTeamID);
                    updateDetailCmd.Parameters.AddWithValue("AwayTeamID", ScheduleDetail.AwayTeamID);
                    updateDetailCmd.Parameters.AddWithValue("WeekNumber", ScheduleDetail.WeekNumber);
                    updateDetailCmd.Parameters.AddWithValue("MatchNumber", ScheduleDetail.MatchNumber);
                    updateDetailCmd.ExecuteNonQuery();
                }
                else
                {
                    //add command
                    MySqlCommand AddDetailCmd = new MySqlCommand("INSERT INTO scheduledetails VALUES(HomeTeamID = ?HomeTeamID, AwayTeamID = ?AwayTeamID, WeekNumber = ?WeekNumber, MatchNumber = ?MatchNumber)", conn);
                    //values
                    AddDetailCmd.Parameters.AddWithValue("HomeTeamID", ScheduleDetail.HomeTeamID);
                    AddDetailCmd.Parameters.AddWithValue("AwayTeamID", ScheduleDetail.AwayTeamID);
                    AddDetailCmd.Parameters.AddWithValue("WeekNumber", ScheduleDetail.WeekNumber);
                    AddDetailCmd.Parameters.AddWithValue("MatchNumber", ScheduleDetail.MatchNumber);
                    AddDetailCmd.ExecuteNonQuery();
                }
            }
        }
    }
}
