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

        public VisaInfoController(
            ILogger<VisaInfoController> logger,
            IRepository repository,
            StatisticBuilder statisticBuilder)
        {
            _logger = logger;
            this.repository = repository;
            this.statisticBuilder = statisticBuilder;
        }

        [HttpGet]
        public VisaStatisticItem[] Get(string city)
        {
            var filter = new VisaInfoFilter(city);
            var infos = repository.Read(filter);
            var statistic = statisticBuilder.BuildStatisticByDays(infos);
            return statistic;
        }
    }
}