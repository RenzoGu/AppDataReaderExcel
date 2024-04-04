using Microsoft.EntityFrameworkCore;

namespace AppDataReaderExcel.Models
{
    public class ContextDataBase : DbContext
    {
        public ContextDataBase(DbContextOptions<ContextDataBase> options) : base(options)
        {
        }

        public virtual DbSet<Acta> acta { get; set; }
    }
}
