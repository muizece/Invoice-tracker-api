namespace WoqodData.Data
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> GetData<T, P>(string query, P parameters, CommandType c,
            string connectionId = "default");

        Task SaveData<P>(string query, P parameters, 
            string connectionId = "default");
    }

    // Enum declaration
    public enum CommandType
    {
        Text,            // Represents a raw SQL command
        StoredProcedure, // Represents a stored procedure
    }

}