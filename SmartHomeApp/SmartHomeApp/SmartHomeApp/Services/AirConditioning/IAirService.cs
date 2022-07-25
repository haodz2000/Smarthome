using SmartHomeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeApp
{
    public interface IAirService
    {
        Task<AirConditioning> RefreshDataAsync(string url);

        Task SaveAirAsync(AirConditioning item, bool isNewItem);

        Task DeleteAirAsync(string id);
    }
}
