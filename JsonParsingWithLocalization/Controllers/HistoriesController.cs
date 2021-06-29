using Business.Abstract;
using Entities.Concrete;
using JsonParsingWithLocalization.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Resources;

namespace JsonParsingWithLocalization.Controllers
{
    public class HistoriesController : Controller
    {
        private readonly ILogger<HistoriesController> _logger;
        private readonly IHistoryService _historyService; // Minimize dependency
        private readonly IStringLocalizer<HistoriesController> _localizer;
        private readonly ResourceManager _resourceManager;

        public HistoriesController(ILogger<HistoriesController> logger, IHistoryService historyService, IStringLocalizer<HistoriesController> localizer,
            ResourceManager resourceManager)
        {
            _logger = logger;
            _historyService = historyService;
            _localizer = localizer;
            _resourceManager = resourceManager;
        }

        public IActionResult Index()
        {
            var viewModel = new MyViewModel
            {
                ListTurkish = _historyService.GetAllByTurkish(),
                ListItalian = _historyService.GetAllByItalian()
            };

            var names = _localizer["ID","Date","Category","Event", "Title"];
            ViewData["tableColumnNames"] = names;

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult CultureManagement(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) }
            );

            return LocalRedirect(returnUrl);
        }

        [HttpGet("getallbytr")] //getallbytr
        public IActionResult GetAllByTurkish()
        {
            try
            {
                //throw new Exception(); // getallbytr?culture=tr
                var result = _historyService.GetAllByTurkish();
                return Ok(result); // Created 201 or Ok 200 with HttpGet
            }
            catch (Exception e)
            {
                _logger.LogError(BuildLogInfo(nameof(GetAllByTurkish), $"UnexpectedServerError - {e.Message}"));
                return StatusCode(StatusCodes.Status500InternalServerError, BuildStringFromResource("UnexpectedServerError"));
            }
        }

        [HttpGet("getallbyit")] //getallbyit
        public IActionResult GetAllByItalian()
        {
            try
            {
                var result = _historyService.GetAllByItalian();
                return Ok(result); // Created 201 or Ok 200 with HttpGet
            }
            catch (Exception e)
            {
                _logger.LogError(BuildLogInfo(nameof(GetAllByItalian), $"UnexpectedServerError - {e.Message}"));
                return StatusCode(StatusCodes.Status500InternalServerError, BuildStringFromResource("UnexpectedServerError"));
            }
        }

        [HttpGet("getbyturkishid")] //getbyturkishid?id=num
        public IActionResult GetByIdInTurkishList(int id)
        {
            try
            {
                var result = _historyService.GetByIdInTurkishList(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(BuildLogInfo(nameof(GetByIdInTurkishList), $"UnexpectedServerError - {e.Message}", id));
                return StatusCode(StatusCodes.Status500InternalServerError, BuildStringFromResource("UnexpectedServerError"));
            }
        }

        [HttpGet("getbyitalianid")] //getbyitalianid?id=num
        public IActionResult GetByIdInItalianList(int id)
        {
            try
            {
                var result = _historyService.GetByIdInItalianList(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(BuildLogInfo(nameof(GetByIdInItalianList), $"UnexpectedServerError - {e.Message}", id));
                return StatusCode(StatusCodes.Status500InternalServerError, BuildStringFromResource("UnexpectedServerError"));
            }
        }

        [HttpPost("add")] //add
        public IActionResult Add(History history)
        {
            try
            {
                _historyService.Add(history);
                return Ok(history);
            }
            catch (Exception e)
            {
                _logger.LogError(BuildLogInfo(nameof(Add), $"UnexpectedServerError - {e.Message}", history));
                return StatusCode(StatusCodes.Status500InternalServerError, BuildStringFromResource("UnexpectedServerError"));
            }
        }

        [HttpPut("update")] //update
        public IActionResult Update(History history)
        {
            try
            {
                _historyService.Update(history);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(BuildLogInfo(nameof(Update), $"UnexpectedServerError - {e.Message}", history));
                return StatusCode(StatusCodes.Status500InternalServerError, BuildStringFromResource("UnexpectedServerError"));
            }
        }

        [HttpDelete("delete")] //delete
        public IActionResult Delete(History history)
        {
            try
            {
                _historyService.Delete(history);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(BuildLogInfo(nameof(Delete), $"UnexpectedServerError - {e.Message}", history));
                return StatusCode(StatusCodes.Status500InternalServerError, BuildStringFromResource("UnexpectedServerError"));
            }
        }

        [HttpPost("importjson")] //importjson?lang=tr
        public IActionResult ImportJson(string lang)
        {
            try
            {
                _historyService.ImportJson(lang);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(BuildLogInfo(nameof(ImportJson), $"UnexpectedServerError - {e.Message}", lang));
                return StatusCode(StatusCodes.Status500InternalServerError, BuildStringFromResource("UnexpectedServerError"));
            }
        }

        // Builds up a string looking up a resource and doing the replacements.
        // "resourceStringName" > Name of resource to use
        // "replacements" > Strings to use for replacing in the resource string
        private string BuildStringFromResource(string resourceStringName, params object[] replacements)
        {
            // Localization: Here we are using the more clasic way of getting resources using the ResourceManager instead of the IStringLocalizer
            // to look up resource strings from the .resx files. Get the appropriate resource based on the request culture.
            return string.Format(_resourceManager.GetString(resourceStringName), replacements);
        }

        // Builds up a log string using the parameters passed in
        // "methodName" > Name of method logging from
        // "resourceStringName" > Name of resource to use
        // "replacements" > Strings to use for replacing in the resource string
        private string BuildLogInfo(string methodName, string resourceStringName, params object[] replacements)
        {
            // Localization: Here we are using .NET Core's IStringLocalizer to get the localized strings from the .resx files instead of
            // the ResourceManager. Get the appropriate resource based on the request culture.
            return $"{methodName}: {_localizer[resourceStringName, replacements]}";
        }
    }
}