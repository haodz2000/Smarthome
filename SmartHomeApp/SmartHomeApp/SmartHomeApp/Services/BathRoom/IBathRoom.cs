using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartHomeApp.Models;
namespace SmartHomeApp
{
    public interface IBathRoom
    {
        Task<BathRoom> RefreshDataAsync(string url);

        Task SaveRoomAsync(BathRoom item, bool isNewItem);

        Task DeleteRoomAsync(string id);
    }
}
