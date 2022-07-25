using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartHomeApp.Models;
namespace SmartHomeApp
{
    public interface IHomeService
    {
        Task<Home> RefreshDataAsync(string url);

        Task SaveHomeAsync(Home item, bool isNewItem);

        Task DeleteHomeAsync(string id);
    }
}
