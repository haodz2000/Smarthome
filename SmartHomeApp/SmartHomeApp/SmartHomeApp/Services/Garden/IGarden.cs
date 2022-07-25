using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartHomeApp.Models;
namespace SmartHomeApp
{
    public interface IGarden
    {
        Task<Garden> RefreshDataAsync(string url);

        Task SaveRoomAsync(Garden item, bool isNewItem);

        Task DeleteRoomAsync(string id);
    }
}
