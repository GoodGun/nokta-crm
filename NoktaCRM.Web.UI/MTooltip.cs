using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Xml;
using Utility;

namespace NoktaCRM.Web.UI
{
    public enum TooltipPosition
    {
        RightUp = 0,
        RightDown = 1,
        LeftUp = 2,
        LeftBottom = 3
    }

    [DefaultProperty("ResourceKey"), ToolboxData(@"<{0}:MTooltip runat=""server""></{0}:MTooltip>")]
    public class MTooltip : Literal
    {
        public string ResourceKey
        {
            get { return _ResourceKey; }
            set { _ResourceKey = value; }
        }private string _ResourceKey = "";

        public TooltipPosition Position 
        {
            get{return _Position;} 
            set{_Position = value;} 
        }
        private TooltipPosition _Position = TooltipPosition.RightDown;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (string.IsNullOrEmpty(ResourceKey)) return;

            try { this.Text = GetTooltipData(ResourceKey, Position);}
            catch { this.Text = ResourceKey; }
        }

        public static string GetTooltipData(string key, TooltipPosition position = TooltipPosition.RightDown)
        {
            string pos = "";

            switch (position)
            {
                case TooltipPosition.LeftBottom: pos = "lb";break;
                case TooltipPosition.LeftUp: pos = "lu";break;
                case TooltipPosition.RightDown: pos = "rb";break;
                case TooltipPosition.RightUp: pos = "ru";break;
            }
            return string.Format(@"<a class='tooltip' href='javascript://'>?<span class='{0}'>{1}</span></a>",
                pos,
                ResourceManager.GetResource(key)); 
        }
    }
}
