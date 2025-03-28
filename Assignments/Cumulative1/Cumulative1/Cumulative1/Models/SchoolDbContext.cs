using MySql.Data.MySqlClient;
//This namespace sometimes shows up as unrecognized....if so navigate to project dir then in cmd ->
//dotnet add package MySql.Data -v 8.0

namespace Cumulative1.Models
{
    public class SchoolDbContext
    {
        private static string User { get { return "dhruv"; } }
        private static string Password { get { return "1234"; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                     + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True"
                    + "; SslMode=None";
            }
        }
        /// <summary>
        /// Returns a connection to the school database.
        /// </summary>
        /// <example>
        /// private SchoolDbContext School = new SchoolDbContext();
        /// MySqlConnection Conn = School.AccessDatabase();
        /// </example>
        /// <returns>A MySqlConnection Object</returns>
        /// <remarks>This is the method we actually use to get the database!</remarks>
        public MySqlConnection AccessDatabase()
        {
            //We are instantiating the MySqlConnection Class to create an object
            //the object is a specific connection to our school database on port 3306 of localhost
            return new MySqlConnection(ConnectionString);
        }
    }
}
