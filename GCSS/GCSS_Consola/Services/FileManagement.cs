using System;
using System.IO;
using System.Data.OleDb;
using System.Data;
using GCSS_Libreria.Models;

namespace GCSS
{
    class FileManagement
    {
        public static string SetDirectory()
        {
            string path = string.Empty;
            string target = string.Empty;
            try

            {
                // Get the current directory.
                path = Directory.GetCurrentDirectory();
                target = path + @"\Files\";
                if (!Directory.Exists(target))
                {
                    Directory.CreateDirectory(target);
                }

                // Change the current directory
                // Environment.CurrentDirectory = (target);
            }
            catch (Exception e)
            {
                Console.WriteLine("El proceso falló: {0}", e.ToString());
            }
            return target;
        }

        public static string[] GetFilePath(string _path)
        {
            string[] _filespath;
            _filespath = Directory.GetFiles(_path);
            return _filespath;
        }

        public static ReadedFile ReadFile(string _path)
        {
            ReadedFile Me = new ReadedFile
            {
                fileName = Path.GetFileName(_path),
                ReadedData = new DataSet()
            };

            var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _path + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\""; ;
            using (var conn = new OleDbConnection(connectionString))
            {

                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    Console.WriteLine("No se pudo abrir el archivo: {0}", ex.Message);
                    return null;
                }


                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });
                string strTblName = schemaTable.Rows[1].ItemArray[2].ToString();
                strTblName = strTblName.Substring(0, strTblName.IndexOf("$"));

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT * FROM [{0}]", strTblName + "$");
                    var adapter = new OleDbDataAdapter(cmd);
                    try
                    {
                        adapter.Fill(Me.ReadedData);
                    }
                    catch (Exception ex)
                    {
                        if (adapter != null) { adapter.Dispose(); }
                        if (Me.ReadedData != null) { Me.ReadedData.Dispose(); }
                        Console.WriteLine("No se pudo leer el contenido del archivo: {0}", ex.Message);
                        return null;
                    }
                }
            }
            return Me;
        }
    }
}
