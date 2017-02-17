using System;
using System.Data;

/// <summary>
/// Summary description for Activity
/// </summary>
namespace BusinessObjects
{
    public class Activity
    {
        #region Constructors
        public Activity()
        {
        }
        #endregion

        #region Variables
        private int _ActivityID;
        private int _OrderID = 0;
        private string _ActivityCode ;
        private string _Name ;
        private string _Description ;
        private int _MemberID = 0;
        private int _CustomerID = 0;
        private int _ContactID = 0;
        private int _ActivityTypeID = 0;
        private int _ActivityStatusID = 0;
        private int _CreatedBy = 0;
        private DateTime _ActivityDate = System.DateTime.Now;
        private DateTime _CreateDate = System.DateTime.Now;
        private DateTime _UpdateDate ;

        #endregion

        #region Properties
        public int ActivityID { get { return _ActivityID; } set { _ActivityID = value; } }
        public int OrderID { get { return _OrderID; } set { _OrderID = value; } }
        public string ActivityCode { get { return _ActivityCode; } set { _ActivityCode = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Description { get { return _Description; } set { _Description = value; } }
        public int MemberID { get { return _MemberID; } set { _MemberID = value; } }
        public int CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
        public int ContactID { get { return _ContactID; } set { _ContactID = value; } }
        public int ActivityTypeID { get { return _ActivityTypeID; } set { _ActivityTypeID = value; } }
        public int ActivityStatusID { get { return _ActivityStatusID; } set { _ActivityStatusID = value; } }
        public int CreatedBy { get { return _CreatedBy; } set { _CreatedBy = value; } }
        public DateTime ActivityDate { get { return _ActivityDate; } set { _ActivityDate = value; } }
        public DateTime CreateDate { get { return _CreateDate; } set { _CreateDate = value; } }
        public DateTime UpdateDate { get { return _UpdateDate; } set { _UpdateDate = value; } }

        #endregion
    }
}