using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlashMoneyApi.Services
{
    public interface IFlashHttpClient
    {
        HttpClient GetClient();
    }
}
