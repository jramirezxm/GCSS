using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCSS_Libreria.Models
{
    public class RegistroArchivo
    {
        public int ID { get; set; }
        public string CheckSum { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
