using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Core.Interface
{
    public interface ICurrencyConverter
    {
        Task<double> ConvertCurrencyAsync(string fromCurrency, string toCurrency);
    }
}
