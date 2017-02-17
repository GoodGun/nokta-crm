using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Xml;
using Utility;

namespace NoktaCRM.Web.UI
{
    [DefaultProperty("Text"), ToolboxData(@"<{0}:MRadiolist runat=""server""></{0}:MRadiolist>")]
    public class MRadiolist : RadioButtonList
    {
        public string keyTooltip { get; set; }
        public string ListType { get; set; }
        public bool AddAllItem { get; set; }
        public int scrollHeight { get; set; }
        public int? DefaultSelection { get; set; }
        public string divClass { get; set; }
        public int MaxSelectedItems { get; set; }

        #region Functions
        public int IntSelectedValue
        {
            get { return this.SelectedIndex < 0 ? 0 : this.SelectedValue.ToInt(); }
        }
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
            if (AddAllItem)
                try
                {
                    this.Items.Insert(0, new ListItem(ResourceManager.GetResource("all"), ""));
                    this.SelectedIndex = 0;
                }
                catch {}
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

        public bool Checked
        {
            get { return this.IntSelectedValue.ToBool(); }
            set { this.SelectedValue = value ? "1" : "0"; }
        }
        protected override void OnInit(System.EventArgs e)
        {
            try
            {
                GenerateItems();
                base.OnInit(e);
            }
            catch { }
        }
        protected override void OnPagePreLoad(object sender, EventArgs e)
        {
            if (DefaultSelection.HasValue && this.SelectedIndex < 0)
                this.Select(DefaultSelection.Value);
            base.OnPagePreLoad(sender, e);
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
