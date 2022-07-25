using SmartHomeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeApp
{
    public class HomeManager
    {
		IHomeService restService;

		public HomeManager(IHomeService service)
		{
			restService = service;
		}

		public Task<Home> GetTasksAsync(string url)
		{
			return restService.RefreshDataAsync(url);
		}

		public Task SaveTaskAsync(Home item, bool isNewItem = false)
		{
			return restService.SaveHomeAsync(item, isNewItem);
		}	

		public Task DeleteTaskAsync(Home item)
		{
			return restService.DeleteHomeAsync(item._id);
		}
	}
}
