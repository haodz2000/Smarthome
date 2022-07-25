using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartHomeApp.Models;
namespace SmartHomeApp
{
    public interface IKitchen
    {
        Task<Kitchen> RefreshDataAsync(string url);

        Task SaveRoomAsync(Kitchen item, bool isNewItem);

        Task DeleteRoomAsync(string id);
    }
}
