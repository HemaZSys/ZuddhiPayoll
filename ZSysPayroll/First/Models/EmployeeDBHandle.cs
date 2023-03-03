using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace First.Models
{
    public class EmployeeDBHandle
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["ConnDb"].ToString();
            con = new SqlConnection(constring);
        }
        
        // **************** ADD NEW STUDENT *********************
        public bool AddStudent(Employee e)
        {
            connection();
            SqlCommand cmd = new SqlCommand("SP_Employee", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", e.Id);
            cmd.Parameters.AddWithValue("@Name", e.Name);
            cmd.Parameters.AddWithValue("@Designation", e.Designation);
            cmd.Parameters.AddWithValue("@EmployeeId", e.EmployeeId);
            cmd.Parameters.AddWithValue("@Dateofjoin", e.DateofJoin);
            cmd.Parameters.AddWithValue("@Gender", e.Gender);
            cmd.Parameters.AddWithValue("@Education", e.Education);
            cmd.Parameters.AddWithValue("@Address", e.Address);
            cmd.Parameters.AddWithValue("@PAN", e.PAN);
            cmd.Parameters.AddWithValue("@Aadhar", e.Aadhar);
            cmd.Parameters.AddWithValue("@Passport", e.Passport);
            cmd.Parameters.AddWithValue("@status", "INSERT");

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
       
        // ********** VIEW STUDENT DETAILS ********************
        public List<Employee> GetStudent()
        {
            connection();
            List<Employee> employeelist = new List<Employee>();

            SqlCommand cmd = new SqlCommand("GetEmployeeDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                employeelist.Add(
                    new Employee
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        Designation=Convert.ToString(dr["Designation"]),
                        EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                        DateofJoin =Convert.ToString(dr["Dateofjoin"]),
                        Gender=Convert.ToString(dr["Gender"]),
                        Education=Convert.ToString(dr["Education"]),
                        Address=Convert.ToString(dr["Address"]),
                        PAN=Convert.ToString(dr["PAN"]),
                        Aadhar=Convert.ToString(dr["Aadhar"]),
                        Passport=Convert.ToString(dr["Passport"])
                    });
            }
            return employeelist;
        }

        // ***************** UPDATE STUDENT DETAILS *********************
        public bool UpdateDetails(Employee e)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateStudentDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", e.Id);
            cmd.Parameters.AddWithValue("@Name", e.Name);
            cmd.Parameters.AddWithValue("@Designation", e.Designation);
            cmd.Parameters.AddWithValue("@EmployeeId", e.EmployeeId);
            cmd.Parameters.AddWithValue("@Dateofjoin", e.DateofJoin);
            cmd.Parameters.AddWithValue("@Gender", e.Gender);
            cmd.Parameters.AddWithValue("@Education", e.Education);
            cmd.Parameters.AddWithValue("@Address", e.Address);
            cmd.Parameters.AddWithValue("@PAN", e.PAN);
            cmd.Parameters.AddWithValue("@Aadhar", e.Aadhar);
            cmd.Parameters.AddWithValue("@Passport", e.Passport);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // ********************** DELETE STUDENT DETAILS *******************
        public bool DeleteStudent(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

    }
}