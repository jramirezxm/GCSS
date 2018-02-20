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
                //Establece la carpeta de donde leer los archivos.
                //Está definido que lea los archivos desde la carpeta documentos del usuario que ejecuta el proceso.
                //Si la carpeta de archivos no existe el programa no se ejecuta.
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                target = path + @"\ArchivosCartera\";
                if (!Directory.Exists(target))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n¡ERROR!");
                    Console.ResetColor();
                    Console.WriteLine("No Existe el directorio \"ArchivosCartera\" en la ruta:\n {0} \n", path);
                    Console.WriteLine("Por favor cree el directorio en la ruta señalada \ny agregue allí los archivos de cartera a procesar.");
                    Console.WriteLine("\nEl programa se cerrará");
                    System.Threading.Thread.Sleep(10000);

                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Leyendo archivos de: {0} \n", target);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("El proceso falló:\n {0}\n", e.ToString());
            }
            return target;
        }

        public static string[] GetFilesGroup(string _path)
        {
            string[] _filespath;
            _filespath = Directory.GetFiles(_path);
            return _filespath;
        }

        public static string GetFullFileName(string _path)
        {
            string name;
            bool r = false;
            string file = Path.GetFileName(_path);
            name = Path.GetFileNameWithoutExtension(_path);

            if (name.Length == 8)
            {
                name = string.Concat(name, "0000");
                r = true; ;
            }
            else
            {
                if (name.Length == 12)
                {
                    r = true;
                }
                else
                {
                    r = false;
                }
            }
            
            if (!r)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n¡ERROR!");
                Console.ResetColor();
                Console.WriteLine("El nombre del archivo \"{0}\" no cumple con los requisitos para este programa.\n", name);
                Console.WriteLine("Recuerde que el archivo debe nombrarse segun la fecha en que fue creado.");
                Console.WriteLine("Debe referirse a una fecha válida.");
                Console.WriteLine("El nombre se crea segun el formato: \"aaaammdd.xls\".");
                Console.WriteLine("Tambien es válido usar fecha, hora (formato 24 horas) y minutos \"aaaammddHHmm.xls\".\n");
                Console.WriteLine("Según la fecha y hora que use en el nombre de archivo será el orden de procesamiento del mismo.\n");
                Console.WriteLine("Por favor renombrelo para poder ejecutar la aplicación correctamente.");
                Console.WriteLine("\nEl programa se cerrará.");
                System.Threading.Thread.Sleep(20000);

                Environment.Exit(0);
            }
            return name;
        }
        
        public static bool CheckNameValidity(string _name)
        {
            bool r = true;
            string name = _name;
            //int year;
            //int month;
            //int day;
            //int hora;
            //int minutos;

            if (r && int.TryParse(name.Substring(0, 4), out int year))
            {
                if (year < 1900 || year > DateTime.Now.Year)
                {
                    r = false;
                }
            }
            else
            {
                r = false;
            }

            if(r && int.TryParse(name.Substring(4, 2), out int month))
            {
                if (month < 1 || month > 12)
                {
                    r = false;
                }
            }
            else
            {
                r = false;
            }

            if (r && int.TryParse(name.Substring(6, 2), out int day))
            {
                if (day < 1 || day > 31)
                {
                    r = false;
                }
            }
            else
            {
                r = false;
            }

            if (r && int.TryParse(name.Substring(8, 2), out int hora))
            {
                if (hora < 0 || hora > 23)
                {
                    r = false;
                }
            }
            else
            {
                r = false;
            }

            if (r && int.TryParse(name.Substring(10, 2), out int minutos))
            {
                if (minutos < 0 || minutos > 59)
                {
                    r = false;
                }
            }
            else
            {
                r = false;
            }


            if (!r)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n¡ERROR!");
                Console.ResetColor();
                Console.WriteLine("El nombre del archivo \"{0}\" no cumple con los requisitos para este programa.\n", _name);
                Console.WriteLine("Recuerde que el archivo debe nombrarse segun la fecha en que fue creado.");
                Console.WriteLine("Debe referirse a una fecha válida.");
                Console.WriteLine("El nombre se crea segun el formato: \"aaaammdd.xls\".");
                Console.WriteLine("Tambien es válido usar fecha, hora (formato 24 horas) y minutos \"aaaammddHHmm.xls\".\n");
                Console.WriteLine("Según la fecha y hora que use en el nombre de archivo será el orden de procesamiento del mismo.\n");
                Console.WriteLine("Por favor renombrelo para poder ejecutar la aplicación correctamente.");
                Console.WriteLine("\nEl programa se cerrará.");
                System.Threading.Thread.Sleep(20000);

                Environment.Exit(0);
            }

            return r;
        }

        public static ReadedFile ReadFile(string _path)
        {
            ReadedFile Me = new ReadedFile
            {
                fileName = Path.GetFileName(_path),
                filepath = _path,
                ReadedData = new DataSet()
            };

                var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _path + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\""; ;
            using (var conn = new OleDbConnection(connectionString))
            {
                bool isValid = true;
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n¡ERROR!");
                    Console.ResetColor();
                    Console.WriteLine("No se pudo abrir el archivo:\n{0}\nEl archivo no es un archivo de Excel válido.\n{1}", _path, ex.Message);
                    return null;
                }


                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new Object[] { null, null, null, "TABLE" });

                string strTblName="";
                foreach (DataRow row in schemaTable.Rows)
                {
                    strTblName = row.ItemArray[2].ToString();
                    strTblName = strTblName.Substring(0, strTblName.IndexOf("$"));
                    if(strTblName =="datos")
                    {
                        isValid = true;
                        break;
                    }
                    else
                    {
                        isValid = false;
                    }
                }

                //string strTblName = schemaTable.Rows[1].ItemArray[2].ToString();
                if (!isValid)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n¡ERROR!");
                    Console.ResetColor();
                    Console.WriteLine("El archivo no contiene una hoja llamada \"datos\" que se pueda leer.\n");
                    Environment.Exit(0);
                }                

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
                        Console.WriteLine("No se pudo leer el contenido del archivo: {0}",ex.Message);
                        return null;
                    }
                }
            }
            return Me;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
