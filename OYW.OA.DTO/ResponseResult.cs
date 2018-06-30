using System;
using System.Collections.Generic;
using System.Text;

namespace OYW.OA.DTO
{
    public class ResponseResult<T>
    {
        public bool Succeed { get; set; } = true;

        public List<T> rows { get; set; }

        public int total { get; set; }
    }
}
