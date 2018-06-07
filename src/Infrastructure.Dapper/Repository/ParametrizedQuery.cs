namespace Infrastructure.Dapper.Repository
{
    public class ParametrizedQuery
    {
        public ParametrizedQuery(string sql, dynamic parameter = null)
        {
            Sql = sql;
            Parameter = parameter;
        }

        public string Sql { get; }
        public dynamic Parameter { get; }
    }
}
