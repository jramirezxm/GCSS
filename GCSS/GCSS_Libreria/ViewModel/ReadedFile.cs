﻿using System;
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
        public string CheckSum { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DataSet ReadedData { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
