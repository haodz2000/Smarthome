using SmartHomeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeApp
{
    public class LivingRoomManager
    {
		ILivingRoomService restService;

		public LivingRoomManager(ILivingRoomService service)
		{
			restService = service;
		}

		public Task<LivingRoom> GetTasksAsync(string url)
		{
			return restService.RefreshDataAsync(url);
		}

		public Task SaveTaskAsync(LivingRoom item, bool isNewItem = false)
		{
			return restService.SaveRoomAsync(item, isNewItem);
		}	

		public Task DeleteTaskAsync(LivingRoom item)
		{
			return restService.DeleteRoomAsync(item._id);
		}
	}
}
