using Microsoft.EntityFrameworkCore;
using OYW.OA.Domain.People;
using OYW.OA.Domain.Settings;
using OYW.OA.EFRepositories.MapConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace OYW.OA.EFRepositories
{
    public partial class OAEntity : DbContext
    {
        public OAEntity(DbContextOptions<OAEntity> options) : base(options)
        {
        }
        public virtual DbSet<ORG_User> ORG_User { get; set; }
        public virtual DbSet<SYS_Menu> SYS_Menu { get; set; }

        public virtual DbSet<ORG_UserLogon> ORG_UserLogon { get; set; }

        public virtual DbSet<ORG_Department> ORG_Department { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
