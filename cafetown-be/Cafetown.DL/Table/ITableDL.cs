using Cafetown.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.DL.Table
{
    public interface ITableDL : IBaseDL<TableManager>
    {
       Task<List<TableManager>> GetAllFilter(string textFilter);
    }
}
