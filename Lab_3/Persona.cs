using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace Lab04
{
    public partial class Persona : Form
    {
        SqlConnection conn;
        public Persona(SqlConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
        }

        private void Persona_Load(object sender, EventArgs e)
        {

        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            try
                {

                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        String sql = "SELECT * FROM Person";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader reader = cmd.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        dgvListado.DataSource = dt;
                        dgvListado.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("La conexión esta cerrada");
                    }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();

            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                String FirstName = txtNombre.Text;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "BuscarPersonaNombre";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@FirstName";
                param.SqlDbType = SqlDbType.NVarChar;
                param.Value = FirstName;

                cmd.Parameters.Add(param);

                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dgvListado.DataSource = dt;
                dgvListado.Refresh();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();

            }
        }

        private void btnBuscarDataReader_Click(object sender, EventArgs e)
        {

            try
            {
                conn.Open();

                List<Personas> personas = new List<Personas>();

                SqlCommand command = new SqlCommand("BuscarPersonaNombre", conn);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.Value = txtNombre2.Text.Trim();
                parameter1.ParameterName = "@FirstName";
                parameter1.SqlDbType = SqlDbType.VarChar;

                command.Parameters.Add(parameter1);

                //Usando el datareader
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    personas.Add(new Personas
                        {
                            PersonId = Convert.ToInt32(reader["PersonID"]),
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            HireDate = Convert.ToString(reader["HireDate"]),
                            EnrollmentDate = Convert.ToString(reader["EnrollmentDate"]),
                        }
                    );
                }

                dgvListado.DataSource = personas;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();

            }
        }



    }
}
