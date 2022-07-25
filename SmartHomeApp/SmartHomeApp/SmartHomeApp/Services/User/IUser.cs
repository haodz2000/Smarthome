using SmartHomeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeApp
{
    public interface IUser
    {
        Task<User> RefreshDataAsync(string url);

        Task<User> SaveDataAsync(User item,string url ,bool isNewItem);

        Task DeleteHomeAsync(string id);
    }
}
