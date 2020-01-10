using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SsbSample.Data
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext() { }
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
            : base(options) { }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

    }
}
