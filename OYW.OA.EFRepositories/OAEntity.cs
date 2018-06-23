using Microsoft.EntityFrameworkCore;
using OYW.OA.Domain.People;
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

    }
}
