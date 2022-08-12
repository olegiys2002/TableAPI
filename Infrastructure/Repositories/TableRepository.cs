using Core.IServices.IRepositories;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TableRepository : RepositoryBase<Table>,ITableRepository
    {
        private readonly ApplicationContext _applicationContext;
        public TableRepository(ApplicationContext applicationContext) : base (applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<Table> GetTable(int id)
        {
           return await _applicationContext.Tables.FirstOrDefaultAsync(table=>table.Id == id);
        }
    }
}
