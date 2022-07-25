using SmartHomeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeApp
{
    public class KitchenManager
    {
		IKitchen restService;
		public KitchenManager(IKitchen service)
		{
			restService = service;
		}
		public Task<Kitchen> GetTasksAsync(string url)
		{
			return restService.RefreshDataAsync(url);
		}

		public Task SaveTaskAsync(Kitchen item, bool isNewItem = false)
		{
			return restService.SaveRoomAsync(item, isNewItem);
		}

		public Task DeleteTaskAsync(Kitchen item)
		{
			return restService.DeleteRoomAsync(item._id);
		}
	}
}
