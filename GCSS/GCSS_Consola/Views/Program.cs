using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCSS;
using GCSS_Libreria.Models;

namespace GCSS_Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            string _filesdir = string.Empty;
            string[] _filesgroup;

            Console.WriteLine("Iniciando proceso de lectura de archivo(s)...");

            _filesdir = GCSS.FileManagement.SetDirectory();

            _filesgroup = GCSS.FileManagement.GetFilesGroup(_filesdir);

            int qtyfiles = _filesgroup.Count();

            if(qtyfiles==0)
            {
                Console.WriteLine("No se encontraron archivos que procesar.");
                Console.WriteLine("Se cerrará el programa.");
                System.Threading.Thread.Sleep(10000);

                Environment.Exit(0);
            }

            if (qtyfiles != 1)
                //mensaje en plural
                Console.WriteLine("Se encontraron {0} archivos para procesar...", qtyfiles);
            else
                //mensaje en singular
                Console.WriteLine("Se encontraron {0} archivo para procesar...", qtyfiles);

            foreach (string filepath in _filesgroup)
            {
                string namefile;
                bool r = true;
                namefile = GCSS.FileManagement.GetFullFileName(filepath);
                r = GCSS.FileManagement.CheckNameValidity(namefile);
            }

            int cnt = 0;
            foreach (string filepath in _filesgroup)
            {
                bool c = true;
                cnt++;
                Console.WriteLine(System.Environment.NewLine);
                Console.WriteLine("Preparando archivo {0} de {1}", cnt, qtyfiles);

                Console.WriteLine("Abriendo: {0}", Path.GetFileName(filepath));
                ReadedFile rf = GCSS.FileManagement.ReadFile(filepath);

                if (c)
                {
                    c = GCSS.GestionArchivo.SetRegistroBase(rf);
                }

                //if (c)
                //{
                //    c = GCS.GestionTomadores.LeerTomadores(rf);
                //}

                //if (c)
                //{
                //    c = GCS.GestionPolizas.LeerPolizas(rf);
                //}

            };
            Console.WriteLine("Presione una tecla para terminar...");
            Console.ReadKey();
        }
    }
}