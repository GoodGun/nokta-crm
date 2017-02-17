using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Xml;
using Utility;

namespace NoktaCRM.Web.UI
{	
    [DefaultProperty("Text"), ToolboxData(@"<{0}:MCheckboxList runat=""server""></{0}:MCheckboxList>")]
    public class MCheckboxList : CheckBoxList
    { 
        public string keyTooltip { get; set; } 
        public string ListType { get; set; }
        public int scrollHeight { get; set; }
        public string divClass { get; set; }
        public int MaxSelectedItems { get; set; }
    
        #region Functions
        private void GenerateList()
        {
            string lang = "@Name";
            string xQuery = String.Format("Lists/List[@Key='{0}']/Item", ListType.ToString());

            string filePath = string.Format(ConfigManager.Current.pathForLists, Util.CurrentUserLang);

            foreach (XmlNode node in CachedDocumentManager.GetXmlNodeList(filePath, xQuery))
            {
                string v = node.SelectSingleNode("@Value").InnerText;
                string n;
                XmlNode nameNode = node.SelectSingleNode(lang);
                if (nameNode == null) nameNode = node.SelectSingleNode("@Name");
                n = (nameNode == null) ? "Unknown" : nameNode.InnerText;
                
                this.Items.Add(new ListItem(n, v));
            }
        }
        public void ChangeListType(string DDType)
        {
            this.Items.Clear();
            GenerateList();
        }

        public void GenerateItems()
        {
            GenerateItems(this.Items.Count == 0);
        }

        private void GenerateItems(bool CheckItems)
        {
            if (CheckItems)
            {
                this.Items.Clear();
                GenerateList();
            }
        }
        #endregion

        protected override void OnInit(System.EventArgs e)
        {
            try
            {
                GenerateItems();
                base.OnInit(e);
            }
            catch { }
        }

        protected override void Render(System.Web.UI.HtmlTextWriter oWriter)
        {
            this.EnsureChildControls();
            try
            {
                this.AddTooltip(keyTooltip);
            }
            catch { }

            if (scrollHeight > 0)
                oWriter.Write(string.Format("<div {1}style='max-height:{0}px;overflow-y:scroll;padding:2px;'>", scrollHeight,
                    string.IsNullOrEmpty(divClass) ? "" : string.Format("class='{0}' ", divClass)));
            base.Render(oWriter);
            if (scrollHeight > 0)
                oWriter.Write("</div>");
        }
    }
}
