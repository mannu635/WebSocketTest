using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspNetChat
{
    public partial class Index : System.Web.UI.Page
    {
  
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        private delegate void NameCallBack(string varText);
        public string console
        {
            set { myPanel.Controls.Add(new LiteralControl(value)); }
        }
    }
}