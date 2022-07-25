using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartHomeApp.Models;
namespace SmartHomeApp
{
    public interface ILivingRoomService
    {
        Task<LivingRoom> RefreshDataAsync(string url);

        Task SaveRoomAsync(LivingRoom item, bool isNewItem);

        Task DeleteRoomAsync(string id);
    }
}
