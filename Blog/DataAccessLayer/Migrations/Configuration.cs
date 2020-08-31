
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DataAccessLayer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}