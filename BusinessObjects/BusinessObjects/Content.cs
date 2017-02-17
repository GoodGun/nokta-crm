using System;
using System.Data;

/// <summary>
/// Summary description for Content
/// </summary>
namespace BusinessObjects
{
    public class Content
    {
        #region Constructors
        public Content()
        {
        }
        #endregion

        #region Variables
        private int _ContentID;
        private string _Title;
        private string _Body;
        private int _MemberID = 0;
        private DateTime _CreateDate = System.DateTime.Now;

        #endregion

        #region Properties
        public int ContentID { get { return _ContentID; } set { _ContentID = value; } }
        public string Title { get { return _Title; } set { _Title = value; } }
        public string Body { get { return _Body; } set { _Body = value; } }
        public int MemberID { get { return _MemberID; } set { _MemberID = value; } }
        public DateTime CreateDate { get { return _CreateDate; } set { _CreateDate = value; } }

        #endregion
    }
}