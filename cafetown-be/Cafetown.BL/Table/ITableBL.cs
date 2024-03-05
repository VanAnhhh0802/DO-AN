using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafetown.Common.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace Cafetown.BL.Table
{
    public interface ITableBL : IBaseBL<TableManager>
    {
        Task<List<TableManager>> GetAllFilter(string textFilter);
    }
}
