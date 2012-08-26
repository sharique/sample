using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sample.Data;

namespace Sample.Site.Install
{
    public partial class Install1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

		public void SaveButtonClick (object sender, EventArgs e)
		{
			Repository.Instance.SaveSetting("sitename",tbSitename.Text);
			lblMsg.Text = "Settings saved.";
		}
    }
}