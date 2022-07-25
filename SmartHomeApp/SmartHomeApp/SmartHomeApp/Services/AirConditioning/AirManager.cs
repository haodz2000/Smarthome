using SmartHomeApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeApp
{
	public class AirManager
	{
		IAirService restService;

		public AirManager(IAirService service)
		{
			restService = service;
		}

		public Task<AirConditioning> GetTasksAsync(string url)
		{
			return restService.RefreshDataAsync(url);
		}

		public Task SaveTaskAsync(AirConditioning item, bool isNewItem = false)
		{
			return restService.SaveAirAsync(item, isNewItem);
		}

		public Task DeleteTaskAsync(AirConditioning item)
		{
			return restService.DeleteAirAsync(item._id);
		}
	}
}
