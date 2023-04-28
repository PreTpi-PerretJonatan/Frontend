using System;
namespace FitFocus.ViewModels
{
	public class LogoutPopupViewModel : BaseViewModel
	{
		public string Username { get
			{
				return App.CurrentUser.Username;
			}
		}

		public LogoutPopupViewModel()
		{

		}

	}
}

