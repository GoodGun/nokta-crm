using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Xml;
using Utility;

namespace NoktaCRM.Web.UI
{
    public class MCheckbox : CheckBox
    {
        public string keyText { get; set; }
        public string keyTooltip { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            try
            {
                if (!string.IsNullOrEmpty(keyText)) 
                    this.Text = ResourceManager.GetResource(keyText);

                this.AddTooltip(keyTooltip);
            }
            catch { this.Text = keyText; }
        }
    }
}
