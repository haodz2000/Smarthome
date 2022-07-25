using SmartHomeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeApp
{
    public class UserManager
    {
		IUser restService;

		public UserManager(IUser service)
		{
			restService = service;
		}

		public Task<User> GetTasksAsync(string url)
		{
			return restService.RefreshDataAsync(url);
		}

		public Task<User> SaveTaskAsync(User item, string url="", bool isNewItem = false)
		{
			return restService.SaveDataAsync(item, url, isNewItem);
		}

		public Task DeleteTaskAsync(User item)
		{
			return restService.DeleteHomeAsync(item._id);
		}
	}
}
