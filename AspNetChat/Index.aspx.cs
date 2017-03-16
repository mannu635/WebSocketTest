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
        protected static string provider;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void switchsrc_Click(object sender, EventArgs e)

        {
            if(lbl.Text=="Cache")
            {
                provid = "sql";
                lbl.Text = "SQL";
            }
            else
            {
                provid = "cache";
                lbl.Text = "Cache";
            }
        }
        public string provid
        {
            get { return provider; }
            set { provider = value; }
        }
    }
}