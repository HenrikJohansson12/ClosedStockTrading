using Dapper;
using MySqlConnector;
class CustomerDB : DBConnection
{
    public Customer CustomerLogIn (string personalNumber, string password)
    {
        
            var parameters = new DynamicParameters();
            parameters.Add("@PersonalNumber", personalNumber);
            parameters.Add("@Password",password);
            
            string query = "SELECT id AS Id, name AS Name, email_address AS EmailAddress,"+ 
            "personal_number AS PersonalNumber, password AS Password FROM customers "+ 
            "WHERE personal_number = @PersonalNumber AND password = @Password;";


            using (var connection = DBConnect())
            {
                try
                {
                    Customer loggedInCustomer = connection.QuerySingle<Customer>(query, parameters);
                    return loggedInCustomer;

                }

                catch (System.Exception e)
                {
                    throw e;
                    return null;
                }
            }
    }
}