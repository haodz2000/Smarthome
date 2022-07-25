using SmartHomeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeApp
{
    public class GardenManager
    {
        IGarden restService;
        public GardenManager(IGarden service)
        {
            restService = service;
        }
		public Task<Garden> GetTasksAsync(string url)
		{
			return restService.RefreshDataAsync(url);
		}

		public Task SaveTaskAsync(Garden item, bool isNewItem = false)
		{
			return restService.SaveRoomAsync(item, isNewItem);
		}

		public Task DeleteTaskAsync(Garden item)
		{
			return restService.DeleteRoomAsync(item._id);
		}
	}
}
