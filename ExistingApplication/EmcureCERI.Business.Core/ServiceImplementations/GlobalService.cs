namespace EmcureCERI.Business.Core.ServiceImplementations
{
    using EmcureCERI.Business.Contract;
    using Microsoft.AspNetCore.Http;

    public class GlobalService : IGlobalService
    {
        private readonly HttpContext _currentContext;

        #region Default Construtor

        public GlobalService(IHttpContextAccessor httpContextAccessor)
        {
            _currentContext = httpContextAccessor.HttpContext;
        }

        #endregion

        public string GetBaseUrl()
        {
            var request = _currentContext.Request;

            var host = request.Host.ToUriComponent();

            var pathBase = request.PathBase.ToUriComponent();

            return $"{request.Scheme}://{host}{pathBase}";
        }
    }
}
