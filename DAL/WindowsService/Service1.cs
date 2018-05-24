using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WCF;
using Logger;
using DAL;
namespace WindowsService
{
	public partial class Service1 : ServiceBase
	{
		public Service1()
		{
			InitializeComponent();
		}
		ServiceHost sh;
		protected override void OnStart(string[] args)
		{
			try
			{
				sh = new ServiceHost(typeof(AuctionClient));
				sh.Open();
				Log.Logger("Host start work");				
			}
			catch(Exception ex)
			{
				Log.Logger(ex.Message);
			}
		}

		protected override void OnStop()
		{
			if (sh != null)
				sh.Close();
			Log.Logger("Host finish work");
		}
	}
}
