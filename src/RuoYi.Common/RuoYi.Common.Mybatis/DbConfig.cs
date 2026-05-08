namespace RuoYi.Common.Mybatis
{
    public class DbConfig
    {
        public string? ConnectionString { get; set; }
        public DbType DbType { get; set; } = DbType.MySql;
        public bool EnableAutoClose { get; set; } = true;
        public bool IsAutoCloseConnection { get; set; } = true;
        public int CommandTimeOut { get; set; } = 60;
    }

    public enum DbType
    {
        MySql,
        SqlServer,
        PostgreSQL,
        Oracle
    }
}