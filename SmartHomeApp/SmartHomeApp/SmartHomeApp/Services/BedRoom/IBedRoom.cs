using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartHomeApp.Models;
namespace SmartHomeApp
{
    public interface IBedRoom
    {
        Task<BedRoom> RefreshDataAsync(string url);

        Task SaveRoomAsync(BedRoom item, bool isNewItem);

        Task DeleteRoomAsync(string id);
    }
}
