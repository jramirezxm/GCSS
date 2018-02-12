using GCSS_Libreria.Models;
using System;
using System.IO;

namespace GCSS
{
    public class GestionArchivo
    {
        public static bool SetRegistroBase(ReadedFile rf)
        {
            DateTime creation = File.GetCreationTime(@"C:\test.txt");
            //obtener el identificador de archivo y llevar el control de la unicidad de este archivo.
            //la idea es identificar la fecha del archivo y evitar que un archivo anterior sobreescriba información
            //solo el mismo archivo o uno con fecha y hora de creación superior podría procesarse.
            return true;
        }
    }
}