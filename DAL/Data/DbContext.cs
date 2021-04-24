using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAL.Data
{
    public class DBContext : DbContext
    {
        private readonly string _conStr;
        public DBContext()
        {
            var builder = new ConfigurationBuilder();
            string fileName = @"pathdatabase\DatabaseConnection.json";
            builder.AddJsonFile(Path.GetFullPath(fileName));
            IConfigurationRoot config = builder.Build();
            _conStr = config["ConnectionString"];
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conStr);
        }
    }
}
