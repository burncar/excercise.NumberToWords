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
            NumberModel numberModelDto = null;
            if (TempData["NumberModel"] != null)
            {
                var json = TempData["NumberModel"].ToString();

                //numberModelDto = JsonConvert.DeserializeObject<NumberModelDTO>(json);
                numberModelDto = JsonConvert.DeserializeObject<NumberModel>(json);

                var numberData = new NumberModel()
                {
                    Amount = numberModelDto.Amount,
                    InputAmount = numberModelDto.InputAmount,
                    StringAmount = numberModelDto.StringAmount,
                    InputCondition = numberModelDto.InputCondition
                };
                return View(numberData);
            }
            numberModelDto = new NumberModel
            {
                InputCondition = true,
            };
            return View(numberModelDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> 
            Convert([Bind("Amount,StringAmount,InputAmount","InputCondition")] NumberModel numberModel)
        {
           
           
                try
                {
                    string strConverted = "";
                    decimal converted = 0;
                    if (numberModel.InputAmount == null && numberModel.Amount > 0)
                    {
                         strConverted = await _convertNumToWords.ConvertNumber(numberModel.Amount);
                    }
                    else
                    {
                        string amountConverted = "0";
                        converted = await _convertNumToWords.ConvertString(numberModel.InputAmount);
                    }
                    //string amountConverted =  await _convertNumToWords.ConvertNumber(numberModel.Amount);
                 
                    
                    var numberModelDto = new NumberModelDTO()
                    {
                        Amount = numberModel.Amount,
                        InputAmount = numberModel.InputAmount,
                        StringAmount = numberModel.InputCondition ? strConverted:converted.ToString(),
                        InputCondition = numberModel.InputCondition
                    };

                    TempData["NumberModel"] = JsonConvert.SerializeObject(numberModelDto);
                }
                catch (Exception er)
                {
                    return BadRequest();
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
