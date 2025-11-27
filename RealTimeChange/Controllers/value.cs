using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace RealTimeChange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Value : ControllerBase
    {
        private FeatureToggleSettings _currentSettings;
        private readonly ILogger<Value> _logger;

        public Value(IOptionsMonitor<FeatureToggleSettings> featureToggleMonitor, ILogger<Value> logger)
        {
            _currentSettings = featureToggleMonitor.CurrentValue;
            featureToggleMonitor.OnChange(settings =>
            {
                _currentSettings = settings;
                Console.WriteLine($"FeatureXEnabled changed to: {settings.IsFeatureXEnabled}");
            });
            _logger = logger;
        }

        [HttpGet("isFeatureXEnabled")]
        public bool IsFeatureXEnabled()
        {
            return _currentSettings.IsFeatureXEnabled;
        }
    }
}
