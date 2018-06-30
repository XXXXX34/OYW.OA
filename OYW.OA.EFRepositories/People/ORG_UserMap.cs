using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OYW.OA.Domain;
using OYW.OA.Domain.People;
using OYW.OA.EFRepositories.MapConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace OYW.OA.EFRepositories.People
{
    public class ORG_UserMap : EntityMappingConfiguration<ORG_User>
    {
        public override void Map(EntityTypeBuilder<ORG_User> b)
        {
            b.HasKey(p => p.ID);
        }
    }
}
