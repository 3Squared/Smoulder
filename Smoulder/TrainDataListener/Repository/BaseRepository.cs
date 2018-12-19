using System.Data.SqlClient;

namespace TrainDataListener.Repository
{
    public class BaseRepository
    {
        protected SqlConnection GetSqlConnection()
        {
            return new SqlConnection(
                "Data Source=3SQ10071FP;Initial Catalog=NROD-Local;user id=RailSmartUser;Password=plokij;MultipleActiveResultSets=true;");
        }
    }
}