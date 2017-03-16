using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspNetChat
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void switchsrc_Click(object sender, EventArgs e)
        {
            var config = WebConfigurationManager.OpenWebConfiguration("~");

            if (ConfigurationManager.AppSettings["provider"] == "sql")
            {
                config.AppSettings.Settings["provider"].Value = "Cache";
                lbl.Text = "Cache";
            }
            else
            {
                config.AppSettings.Settings["provider"].Value = "sql";
                lbl.Text = "SQL";
            }
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}