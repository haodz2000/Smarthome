using SmartHomeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeApp
{
    public class BedRoomManager
    {
        IBedRoom restService;

        public BedRoomManager(IBedRoom service)
        {
            restService = service;
        }
		public Task<BedRoom> GetTasksAsync(string url)
		{
            return restService.RefreshDataAsync(url);
		}

		public Task SaveTaskAsync(BedRoom item, bool isNewItem = false)
		{
			return restService.SaveRoomAsync(item, isNewItem);
		}

		public Task DeleteTaskAsync(BedRoom item)
		{
			return restService.DeleteRoomAsync(item._id);
		}
	}
}
