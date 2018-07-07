using System;
using Xunit;
using OYW.OA.Infrastructure;
using OYW.OA.Application.People;
using OYW.OA.ApplicationInterface.People;

namespace OYW.OA.XUnitTest
{
    public class UnitTest1
    {
        public UnitTest1()
        {

        }
        [Fact]
        public void Test1()
        {
            var departmentService = IocManager.Instance.Resolve<IDepartmentService>();
            var str = departmentService.LoadDeptTree("fe6258ca-1ade-4a62-ae9f-dca5d7715da0");
            Assert.NotNull(departmentService);
        }
    }
}
