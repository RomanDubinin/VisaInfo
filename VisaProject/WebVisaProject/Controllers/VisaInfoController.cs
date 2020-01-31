using DataContainer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VisaProject;

namespace WebVisaProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VisaInfoController : ControllerBase
    {

        private readonly ILogger<VisaInfoController> _logger;
        private readonly IRepository repository;
        private readonly StatisticBuilder statisticBuilder;
        private readonly IActualityDateProvider actualityDateProvider;

        public VisaInfoController(
            ILogger<VisaInfoController> logger,
            IRepository repository,
            StatisticBuilder statisticBuilder, IActualityDateProvider actualityDateProvider)
        {
            _logger = logger;
            this.repository = repository;
            this.statisticBuilder = statisticBuilder;
            this.actualityDateProvider = actualityDateProvider;
        }

        [HttpGet]
        public VisaStatisticResult Get(string city)
        {
            var actualDate = actualityDateProvider.GetActualityDate(city);
            var filter = new VisaInfoFilter(city);
            var infos = repository.Read(filter);
            var statistic = statisticBuilder.BuildStatisticByDays(infos);
            return new VisaStatisticResult(actualDate, statistic);
        }
    }
}