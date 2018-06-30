using OYW.OA.Domain.People;
using OYW.OA.EFRepositories.MapConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace OYW.OA.EFRepositories.People
{
    public class ORG_UserLogonMap : EntityMappingConfiguration<ORG_UserLogon>
    {
        public override void Map(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ORG_UserLogon> b)
        {
            b.HasKey(p => p.ID);
            b.HasOne(p => p.User).WithMany(p => p.UserLogonList).HasForeignKey(p => p.UserID);
        }
    }
}
