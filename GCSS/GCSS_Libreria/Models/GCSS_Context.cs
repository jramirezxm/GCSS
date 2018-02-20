using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GCSS_Libreria.Models
{
    public partial class GCSS_Context : DbContext
    {


        public GCSS_Context() : base("name=GCSSData")
        {
            Database.SetInitializer<GCSS_Context>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
