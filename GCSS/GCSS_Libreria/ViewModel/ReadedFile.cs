using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCSS_Libreria.Models
{
    public class ReadedFile
    {
        //public ReadedFile()
        //{
        //    this.fileName = string.Empty;
        //    this.ReadedData = null;
        //}
        public string fileName { get; set; }
        public string filepath { get; set; }
        public DataSet ReadedData { get; set; }
        public string CheckSum { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
