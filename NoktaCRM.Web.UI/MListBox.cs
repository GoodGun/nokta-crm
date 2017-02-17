using System;
using System.Collections;
using System.Web.UI.WebControls;
using System.Xml;
using Utility;

namespace NoktaCRM.Web.UI
{
	public class MListBox : ListBox
	{
        public bool SortByText { get; set; }
        public bool ShowChooseItem { get; set; }

        public string keyTooltip { get; set; }
        public string keyRequired { get; set; }
        public string ListType { get; set; }
        public int MaxSelectedItems { get { object v = ViewState["maxItems"]; return v == null ? 0 : v.ToInt(); } set { ViewState["maxItems"] = value; CheckMaxItems(); } }

        #region Utility Functions and Properties
        public int IntSelectedValue
        {
            get { return this.SelectedIndex < 0 ? 0 : this.SelectedValue.ToInt(); }
        }
        public override string SelectedValue
		{
            get { return base.SelectedValue; }
			set
            {
                if (this.Items.FindByValue(value) != null)
                    base.SelectedValue = value;
			}
		}

        public string ValuesToString
		{
			get
			{
				if (this.SelectedIndex == -1)
					return string.Empty;
				else
				{
					string Result = string.Empty;
					for (int i = 0; i < this.Items.Count; i++)
						if (this.Items[i].Selected)
							Result += "," + this.Items[i].Value;
					Result += ",";
					return Result;
				}
			}
		}

		public string TextsToString
		{
			get
			{
				if (this.SelectedIndex == -1)
					return string.Empty;
				else
				{
					string Result = string.Empty;
					for (int i = 0; i < this.Items.Count; i++)
						if (this.Items[i].Selected)
							Result += "," + this.Items[i].Text;
					Result += ",";
					return Result;
				}
			}
		}
        
		public int SelectedItemCount
		{
			get
			{
                int c = 0;
				for (int i = 0; i < this.Items.Count; i++)
					if (this.Items[i].Selected)
                        c++;
                return c;
			}
        }
        private void GenerateList()
        {
            string lang = "@Name";
            string xQuery = String.Format("Lists/List[@Key='{0}']/Item", ListType.ToString());

            ListItem liChoose = null;

            SortedList slItems = null;
            if (SortByText == true)
                slItems = new SortedList();

            string filePath = string.Format(ConfigManager.Current.pathForLists, Util.CurrentUserLang);

            foreach (XmlNode node in CachedDocumentManager.GetXmlNodeList(filePath, xQuery))
            {
                string v = node.SelectSingleNode("@Value").InnerText;
                string n;
                XmlNode nameNode = node.SelectSingleNode(lang);
                if (nameNode == null) nameNode = node.SelectSingleNode("@Name");
                n = (nameNode == null) ? "Unknown" : nameNode.InnerText;

                if (!(ShowChooseItem == false && (n.StartsWith("Seçiniz") || n.StartsWith("Please select"))))
                {
                    if (SortByText == true)
                    {
                        if (!slItems.ContainsKey(n))
                            slItems.Add(n, v);
                    }
                    else
                        this.Items.Add(new ListItem(n, v));
                }
            }
            if (SortByText == true)	//Added for Sorting.
            {
                this.DataSource = slItems;
                this.DataTextField = "Key";
                this.DataValueField = "Value";
                this.DataBind();

                if (this.ShowChooseItem == true)
                {
                    liChoose = this.Items.FindByText(ResourceManager.GetResource("seciniz"));

                    if (liChoose != null)
                    {
                        this.Items.Remove(liChoose);
                        this.Items.Insert(0, liChoose);
                    }
                }
            }
        }

        public void ChangeListType(string DDType)
        {
            this.Items.Clear();
            this.ListType = DDType;
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

        #region Overloads and Utiliy Functions
        private void CheckMaxItems()
        {
            if (MaxSelectedItems > 0)
            {
                this.Attributes.Add("onchange", string.Format("checkMax(this,{0},'{1}');", MaxSelectedItems, ResourceManager.GetResource("max.choose.items")));
            }
        }
		protected override void OnInit(System.EventArgs e)
		{
            try
            {
                CheckMaxItems();
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
            
            base.Render(oWriter);
		}
        #endregion
    }
}
