using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SsbSample.Data
{
    public class WeatherForecastService
    {
        private readonly WeatherDbContext dbContext;
        public WeatherForecastService(WeatherDbContext dbContext) => this.dbContext = dbContext;

        public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            return await dbContext.WeatherForecasts.ToArrayAsync();
        }

        public WeatherForecast[] GetForecast(DateTime startDate)
        {
            return dbContext.WeatherForecasts.ToArray();
        }
    }
}
