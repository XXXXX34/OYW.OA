using Microsoft.EntityFrameworkCore;
using OYW.OA.Domain.People;
using OYW.OA.Domain.Settings;
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
    }
}
