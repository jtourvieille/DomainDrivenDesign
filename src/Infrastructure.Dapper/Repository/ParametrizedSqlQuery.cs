namespace Infrastructure.Dapper.Repository
{
    public class ParametrizedSqlQuery
    {
        public ParametrizedSqlQuery(string sql, dynamic parameter = null)
        {
            Sql = sql;
            Parameter = parameter;
        }

        public string Sql { get; }
        public dynamic Parameter { get; }
    }
}
