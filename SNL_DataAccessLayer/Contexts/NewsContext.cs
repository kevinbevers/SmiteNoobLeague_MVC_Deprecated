using System;
using System.Collections.Generic;
using System.Text;
using SNL_InterfaceLayer.Interfaces;
using SNL_InterfaceLayer.CustomExceptions;
using SNL_InterfaceLayer.DateTransferObjects;
using MySql.Data.MySqlClient;
using System.Linq;

namespace SNL_PersistenceLayer.Contexts
{
    public class NewsContext : IContext<NewsDTO>
    {
        private readonly ConnectionContext _con;
        public NewsContext(ConnectionContext con)
        {
            _con = con;
        }

        //Create
        public void Add(NewsDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO news (NewsTitle,NewsSubject,NewsDate,NewsArticle) " +
                                                        "VALUES(?NewsTitle,?NewsSubject,?NewsDate,?NewsArticle)", conn);
                    //values
                    cmd.Parameters.AddWithValue("NewsTitle", entity.NewsTitle ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("NewsSubject", entity.NewsSubject ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("NewsDate", entity.NewsDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("NewsArticle", entity.NewsArticle ?? (object)DBNull.Value);
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
        //Read
        public IEnumerable<NewsDTO> GetAll()
        {
            try
            {
                List<NewsDTO> newsList = new List<NewsDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT NewsID,NewsTitle,NewsSubject,NewsDate,NewsArticle FROM news", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NewsDTO news = new NewsDTO
                            {
                                NewsID = reader[0] as int? ?? default,
                                NewsTitle = reader[1] as string ?? default,
                                NewsSubject = reader[2] as string ?? default,
                                NewsDate = reader[3] as DateTime? ?? default,
                                NewsArticle = reader[4] as string ?? default,
                            };
                            newsList.Add(news);
                        }
                    }
                }
                return newsList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public NewsDTO GetByID(int? id)
        {
            try
            {
                NewsDTO news = new NewsDTO();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT NewsID,NewsTitle,NewsSubject,NewsDate,NewsArticle FROM news WHERE NewsID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            news.NewsID = reader[0] as int? ?? default;
                            news.NewsTitle = reader[1] as string ?? default;
                            news.NewsSubject = reader[2] as string ?? default;
                            news.NewsDate = reader[3] as DateTime? ?? default;
                            news.NewsArticle = reader[4] as string ?? default;
                        }
                    }
                }
                return news;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Update
        public void Update(NewsDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE news SET(NewsTitle = ?NewsTitle,NewsSubject = ?NewsSubject, NewsDate = ?NewsDate, " +
                                                        "NewsArticle = ?NewsArticle) WHERE NewsID = ?id", conn);
                    //where id is
                    cmd.Parameters.AddWithValue("id", entity.NewsID);
                    //values
                    cmd.Parameters.AddWithValue("NewsTitle", entity.NewsTitle ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("NewsSubject", entity.NewsSubject ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("NewsDate", entity.NewsDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("NewsArticle", entity.NewsArticle ?? (object)DBNull.Value);
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
        //Delete
        public void Remove(NewsDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM news WHERE NewsID = ?id", conn);
                    //where id =
                    cmd.Parameters.AddWithValue("id", entity.NewsID);
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
    }
}
