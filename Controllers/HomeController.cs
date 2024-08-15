using excercise.NumberToWords.Data;
using excercise.NumberToWords.Data.DTO;
using excercise.NumberToWords.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace excercise.NumberToWords.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConvertNumToWords _convertNumToWords;

        public HomeController(ILogger<HomeController> logger, IConvertNumToWords convertNumToWords)
        {
            _logger = logger;
            _convertNumToWords = convertNumToWords; 
        }

        public IActionResult Index()
        {
            Data.DTO.NumberModelDTO numberModelDto = null;
            if (TempData["NumberModel"] != null)
            {
                var json = TempData["NumberModel"].ToString();
                numberModelDto = JsonConvert.DeserializeObject<NumberModelDTO>(json);
                var numberData = new NumberModel()
                {
                    Amount = numberModelDto.Amount,
                    StringAmount = numberModelDto.StringAmount,
                };
                return View(numberData);
            }
            return View(numberModelDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Convert([Bind("Amount,StringAmount")] NumberModel numberModel)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                   
                    string amountConverted =  await _convertNumToWords.ConvertNumber(numberModel.Amount);
                    var numberModelDto = new NumberModelDTO()
                    {
                        Amount = numberModel.Amount,
                        StringAmount = amountConverted
                    };

                    TempData["NumberModel"] = JsonConvert.SerializeObject(numberModelDto);
                }
                catch (Exception er)
                {
                    return BadRequest();
                }

               
            }
            return RedirectToAction(nameof(Index));
            //return View(numberModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
