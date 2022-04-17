using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace WPFSemana04
{
    
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection(@"data source=JORGER\SQLEXPRESS;initial catalog = School; Integrated Security = True;");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Person> people = new List<Person>();

            connection.Open();

            SqlCommand command = new SqlCommand("BuscarPersonaNombre", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.SqlDbType = SqlDbType.VarChar;
            parameter1.Size = 50; 

            parameter1.Value = txtNombre.Text; 
            parameter1.ParameterName = "@FirstName";

            command.Parameters.Add(parameter1);

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                people.Add(new Person
                {
                    PersonId = dataReader["PersonID"].ToString(),
                    LastName = dataReader["LastName"].ToString(),
                    FirstName = dataReader["FirstName"].ToString(),
                    HireDate = dataReader["HireDate"].ToString(),
                    EnrollmentDate = dataReader["EnrollmentDate"].ToString(),
                });

            }
            connection.Close();
            dgvPeople.ItemsSource = people;
        }

        private void dgvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
