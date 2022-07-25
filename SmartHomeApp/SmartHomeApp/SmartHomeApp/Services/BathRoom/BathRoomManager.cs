using SmartHomeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeApp
{
	public class BathRoomManager
	{
		IBathRoom restService;

		public BathRoomManager(IBathRoom service)
		{
			restService = service;
		}
		public Task<BathRoom> GetTasksAsync(string url)
		{
			return restService.RefreshDataAsync(url);
		}

		public Task SaveTaskAsync(BathRoom item, bool isNewItem = false)
		{
			return restService.SaveRoomAsync(item, isNewItem);
		}

		public Task DeleteTaskAsync(BathRoom item)
		{
			return restService.DeleteRoomAsync(item._id);
		}
	}
}
