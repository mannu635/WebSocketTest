﻿using System;
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
            var watch = System.Diagnostics.Stopwatch.StartNew();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
        }
        private delegate void NameCallBack(string varText);
        public void UpdateTextBox(string input)
        {
            if (InvokeRequired)
            {
                textBox.BeginInvoke(new NameCallBack(UpdateTextBox), new object[] { input });
            }
            else
            {
                myPanel.Controls.Add(new LiteralControl(input));
        }

    }
}