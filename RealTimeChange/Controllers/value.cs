using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace RealTimeChange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Value : ControllerBase
    {
        private IOptionsMonitor<FeatureToggleSettings> _currentSettings;

        public Value(IOptionsMonitor<FeatureToggleSettings> featureToggleMonitor)
        {
            _currentSettings = featureToggleMonitor;
            featureToggleMonitor.OnChange(settings =>
            {
                Console.WriteLine($"TestString changed to: {settings.Test}");
            });
        }

        [HttpGet("teststring")]
        public string IsFeatureXEnabled()
        {
            return _currentSettings.CurrentValue.Test;
        }
    }
}
