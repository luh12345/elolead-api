using Elogroup.Lead.Api.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elogroup.Api.Repository
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<StatusLead> StatusLeads { get; set; }
        public DbSet<Lead.Api.Repository.Entities.Lead> Leads { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
    }
}
