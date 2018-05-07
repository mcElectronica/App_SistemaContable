using System;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
//using Oracle.ManagedDataAccess.Client;
//using Oracle.ManagedDataAccess.Types;

namespace ManagedProvider
{

	class Class1
	{

		[STAThread]
		static void Main(string[] args)
		{
            string conString = "User Id=hr; Password=hr; Data Source=localhost:1521/pdborcl; Pooling =false;";

           try
            {
                OracleConnection con = new OracleConnection();
                con.ConnectionString = conString;
                con.Open();


                // Demo: Passing Array Parameters
                // Let's pass array parameters between .NET and Oracle
                // This stored procedure takes an input array and copies its
                // values to the output array

                OracleCommand cmd = new OracleCommand("MYPACK.MYSP", con); 
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter param1 = cmd.Parameters.Add("param1", OracleDbType.Varchar2); 
                OracleParameter param2 = cmd.Parameters.Add("param2", OracleDbType.Varchar2); 
                param1.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                param2.CollectionType = OracleCollectionType.PLSQLAssociativeArray;

                //Setup the parameter direction
                //Note that param2 is NULL

                param1.Direction = ParameterDirection.Input;
                param2.Direction = ParameterDirection.Output;
                param1.Value = new string[3] { "Oracle", "Database", "Rocks" };
                param2.Value = null;

                //Specify the maximum number of elements in the arrays
                // and the maximum size of the varchar2
                param1.Size = 3;
                param2.Size = 3;
                param1.ArrayBindSize = new int[3] { 20, 20, 20 };
                param2.ArrayBindSize = new int[3] { 20, 20, 20 };


                //Execute the statement and output the results

                cmd.ExecuteNonQuery();

               for (int i = 0; i < 3; i++) 
                { 
                    Console.Write((param2.Value as OracleString[])[i]);
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine("Press 'Enter' to continue");
                Console.ReadLine();


                // Demo: Batch SQL, REF Cursors, and MARS
                // Anonymous PL/SQL block embedded in code - executes in one 
                // DB round trip
                // And why don't we try out MARS in Oracle as well 

                string cmdtxt = "BEGIN " + 
                    "OPEN :1 for select FIRST_NAME, DEPARTMENT_ID from EMPLOYEES where DEPARTMENT_ID = 10; " + 
                    "OPEN :2 for select FIRST_NAME, DEPARTMENT_ID from EMPLOYEES where DEPARTMENT_ID = 20; " + 
                    "OPEN :3 for select FIRST_NAME, DEPARTMENT_ID from EMPLOYEES where DEPARTMENT_ID = 30; " + 
                    "END;";

                cmd = new OracleCommand(cmdtxt, con);
                cmd.CommandType = CommandType.Text;


                // ODP.NET has native Oracle data types, such as Oracle REF 
                // Cursors, which can be mapped to .NET data types
                // Bind REF Cursor Parameters for each department
                OracleParameter p1 = cmd.Parameters.Add("refcursor1", OracleDbType.RefCursor);
                p1.Direction = ParameterDirection.Output;

                OracleParameter p2 = cmd.Parameters.Add("refcursor2", OracleDbType.RefCursor);
                p2.Direction = ParameterDirection.Output;

                OracleParameter p3 = cmd.Parameters.Add("refcursor3", OracleDbType.RefCursor);
                p3.Direction = ParameterDirection.Output;
               
                // Execute batched statement

                cmd.ExecuteNonQuery();

                // Let's retrieve data from the 2nd and 3rd parameter without 
                // having to fetch results from the first parameter
                // At the same time, we'll test MARS with Oracle

                OracleDataReader dr1 = ((OracleRefCursor)cmd.Parameters[2].Value).GetDataReader();
                OracleDataReader dr2 = ((OracleRefCursor)cmd.Parameters[1].Value).GetDataReader();

                // Let's retrieve both DataReaders at one time to test if 
                // MARS works
                while (dr1.Read() && dr2.Read())
                {
                    Console.WriteLine("Employee Name: " + dr1.GetString(0) + ", " +
                    "Employee Dept:" + dr1.GetDecimal(1));
                    Console.WriteLine("Employee Name: " + dr2.GetString(0) + ", " +
                    "Employee Dept:" + dr2.GetDecimal(1));
                    Console.WriteLine();
                }
               
                Console.WriteLine("Press 'Enter' to continue");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
            Console.WriteLine(ex.Message);
            }
	    }
	}
}
