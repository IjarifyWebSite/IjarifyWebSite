using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Data.Context
{
    public class IjarifyDbContext : DbContext
    {
        public IjarifyDbContext(DbContextOptions<IjarifyDbContext> options) : base(options)
        {
        }
    }
}
