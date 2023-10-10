using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using EmcureNPD.API.Filters;
using EmcureNPD.API.Helpers.Response;
using EmcureNPD.Business.Core.Interface;
using static System.Net.WebRequestMethods;
using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;


namespace EmcureNPD.API.Controllers.CurrencyConverter
{
    [Route("api/[controller]")]
    [ApiController]
    //[AuthorizeAttribute]
    public class CurrencyConverterController : ControllerBase
    {
        private readonly string apiKey;
        private readonly HttpClient httpClient;
        private readonly string ApiURL;

        private readonly ICurrencyConverter _currencyConverter;
       

        public CurrencyConverterController(ICurrencyConverter currencyConverter)
        {
            this.httpClient = new HttpClient();
            _currencyConverter = currencyConverter ?? throw new ArgumentNullException(nameof(currencyConverter));
        }


        [HttpGet]
        public async Task<IActionResult> ConvertCurrencyAsync(string fromCurrency, string toCurrency)
        {
            var _currencyAmount = await _currencyConverter.ConvertCurrencyAsync(fromCurrency, toCurrency);
            return Ok(_currencyAmount);
        }
    }
}
