using System.Collections.Generic;
using System.Threading.Tasks;
using itsRewards.Models;
using itsRewards.Models.Altrias;

namespace itsRewards.Services.Altrias
{
    public interface IAltriaService
    {
        Task<TokenResponse> AltriaToken();
        Task<string> GetCouponHtml(string brand);
    }
}