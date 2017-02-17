using System;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml;
using Utility;


namespace NoktaCRM.Web.UI
{
    public class MTextbox : TextBox 
    {
        public string keyTooltip { get; set; }
        public string keyPlaceholder { get; set; }
        
        protected override void Render(HtmlTextWriter oWriter)
        {
            try
            {
                this.AddTooltip(keyTooltip);
                this.AddPlaceholder(keyPlaceholder);
            }
            catch { }

            this.EnsureChildControls();
            base.Render(oWriter);
        }
    }
}
