using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class DatabaseFixture : IDisposable
    {
        public SamsungStarsContext _samsungStars { get; private set; }

        public DatabaseFixture()
        {
            // Set up the test database connection and initialize the context
            var options = new DbContextOptionsBuilder<SamsungStarsContext>()
                .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=SamsungStarsTests;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;
            _samsungStars = new SamsungStarsContext(options);
            _samsungStars.Database.EnsureCreated();// create the data base
        }

        public void Dispose()
        {
            // Clean up the test database after all tests are completed
            _samsungStars.Database.EnsureDeleted();
            _samsungStars.Dispose();
        }
    }
}