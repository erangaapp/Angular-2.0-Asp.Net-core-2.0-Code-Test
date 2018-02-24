using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CT.API.Logging
{
    public class SqlHelper
    {
        private string ConnectionString { get; set; }

        public SqlHelper(string connectionStr)
        {
            ConnectionString = connectionStr;
        }

        private async Task<bool> ExecuteNonQuery(string commandStr, List<SqlParameter> paramList)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                using (SqlCommand command = new SqlCommand(commandStr, conn))
                {
                    command.Parameters.AddRange(paramList.ToArray());
                    int count = await command.ExecuteNonQueryAsync();
                    result = count > 0;
                }
            }
            return result;
        }

        public async Task<bool> InsertLog(EventLog log)
        {
            string command = $@"INSERT INTO [dbo].[EventLogs] ([LogLevel],[Message],[CreatedTime],[EventId], [Event]) VALUES (@LogLevel, @Message, @CreatedTime, @EventId, @Event)";
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("LogLevel", log.LogLevel));
            paramList.Add(new SqlParameter("Message", log.Message));
            paramList.Add(new SqlParameter("CreatedTime", log.CreatedTime));
            paramList.Add(new SqlParameter("EventId", log.EventId));
            paramList.Add(new SqlParameter("Event", log.Event));
            return await ExecuteNonQuery(command, paramList);
        }
    }
}
