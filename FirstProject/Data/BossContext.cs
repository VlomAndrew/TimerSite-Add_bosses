using FirstProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using DbContext = System.Data.Entity.DbContext;

namespace FirstProject.Data
{
    public class BossContext : DbContext
    {
        public BossContext() : base("BossContext")
        {

        }

        public static BossContext Create()
        {
            return new BossContext();
            
        }
        public DbSet<BossViewModel> Bosses { get; set; }
        public System.Data.Entity.DbSet<HistoryModel> History { get; set; }

        public System.Data.Entity.DbSet<FirstProject.Models.BossViewModel> BossViewModels { get; set; }
    }

}