namespace FoodMenuManagement.Helper;

public static class DatabaseErrorHandler
{
    public static string GetErrorMessage(Exception ex)
    {
        return ex switch
        {
            Microsoft.Data.SqlClient.SqlException =>
                "Database connection or SQL operation failed.",

            Microsoft.EntityFrameworkCore.DbUpdateException =>
                "Unable to save changes to the database.",

            TimeoutException =>
                "The database operation timed out.",

            _ => "An unexpected database error occurred."
        };
    }
}