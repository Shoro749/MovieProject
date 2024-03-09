using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class SqlRepository : IRepository<MovieEntity>
    {
        private readonly string _connectionString;
        public SqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Add(MovieEntity entity)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    sqlConnection.Open();

                    SqlCommand insert = new SqlCommand(
                        $"insert Movie values ('{entity.Name}', '{entity.Time}:00:00', '{entity.Genre}', {entity.Year})", sqlConnection);
                    insert.ExecuteNonQuery();
                }
            }
            catch(Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        public IEnumerable<MovieEntity> GetAll()
        {
            using(var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                //List<MovieEntity> list = new List<MovieEntity>();

                SqlCommand select = new SqlCommand("select * from Movie", sqlConnection);
                SqlDataReader reader = select.ExecuteReader();

                while (reader.Read())
                {
                    yield return new MovieEntity()
                    {
                        Id = reader.GetFieldValue<int>(0),
                        Name = reader.GetFieldValue<string>(1),
                        Time = (int)reader.GetFieldValue<TimeSpan>(2).Hours,
                        Genre = reader.GetFieldValue<string>(3),
                        Year = reader.GetFieldValue<int>(4),
                    };
                }

                //return list;
            }
        }

        public void Remove(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                SqlCommand delete = new SqlCommand($"delete from Movie where id={id};", sqlConnection);
                delete.ExecuteNonQuery();
            }
        }

        public void Update(int id, MovieEntity entity)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                SqlCommand update = new SqlCommand(
                    $"update Movie set Name=('{entity.Name}', Time='{entity.Time}:00:00', Genre='{entity.Genre}', Year={entity.Year}) where id={id};", sqlConnection);
                update.ExecuteNonQuery();
            }
        }

        public string GetById(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                SqlCommand getbyid = new SqlCommand("select * from Movie where id = @id", sqlConnection);
                getbyid.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = getbyid.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string columnNameValue = reader.GetFieldValue<string>(reader.GetOrdinal("ColumnName"));
                        return columnNameValue;
                    }
                }
                return "Nothing";
            }
        }
    }
}
