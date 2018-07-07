using OYW.OA.Domain.People;
using OYW.OA.EFRepositories.MapConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace OYW.OA.EFRepositories.People
{
    public class ORG_DepartmentMap : EntityMappingConfiguration<ORG_Department>
    {
        public override void Map(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ORG_Department> b)
        {
            b.HasKey(p => p.DeptID);
        }
    }
}
