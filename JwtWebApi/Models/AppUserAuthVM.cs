using JwtWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileServer.Model
{
	public class AppUserAuthVM
	{
		public AppUserAuthVM() : base()
		{
			UserName = "Not authorized";
			BearerToken = string.Empty;
		}

		public string UserName { get; set; }
		public string BearerToken { get; set; }

        public List<AppUserClaimVm> Claims { get; set; }

		public bool IsAuthenticated { get; set; }
		//public bool CanAccessProducts { get; set; }
		//public bool CanAddProduct { get; set; }
		//public bool CanSaveProduct { get; set; }
		//public bool CanAccessCategories { get; set; }
		//public bool CanAddCategory { get; set; }
	}
}
