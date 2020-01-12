using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SsbSample.Data
{
    public class WeatherForecastService
    {
        /// <summary>
        /// Set the database sample size
        /// </summary>
        int RecordsToFetch => 20000;
        private readonly WeatherDbContext dbContext;
        public WeatherForecastService(WeatherDbContext dbContext) => this.dbContext = dbContext;

        public IQueryable<WeatherForecast> Query(DateTime startDate) =>
         dbContext.WeatherForecasts.Where(wf => wf.Date >= startDate).Take(RecordsToFetch);

        public async Task<WeatherForecast[]> GetForecastAsync(DateTime startDate) => 
            await Query(startDate).ToArrayAsync();

        public WeatherForecast[] GetForecast(DateTime startDate) =>
            Query(startDate).ToArray();
    }

}
