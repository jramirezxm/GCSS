using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCSS_Libreria.Models;

namespace GCSS_Libreria.EntityConfigurations
{
    class RegistroArchivoConfiguration : EntityTypeConfiguration<RegistroArchivo>
    {
        public RegistroArchivoConfiguration()
        {
            ToTable("RegistroArchivos");

            HasKey(r => r.ID)
            .Property(r => r.ID)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(r => r.CheckSum)
                .HasColumnName("CheckSum")
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                        new IndexAnnotation(
                            new IndexAttribute("IX_CheckSum", 1) { IsUnique = true }));


        }
    }
}
