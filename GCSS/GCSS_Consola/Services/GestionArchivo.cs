using GCSS_Libreria.Models;
using System;
using System.IO;
using System.Security.Cryptography;

namespace GCSS
{
    public class GestionArchivo
    {
        public static string GetHash(string path)
        {
            DateTime creation = File.GetCreationTime(path);
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-","").ToLowerInvariant();
                }
            }
        }

        public static DateTime GetCreationTime(string path)
        {
            return File.GetCreationTime(path);
        }

        public static bool checkDuplicity(ReadedFile rf)
        {
            return true;
        }

        public static bool SetRegistroBase(ReadedFile rf)
        {

            return true;
        }
    }
}