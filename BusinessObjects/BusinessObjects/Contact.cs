using System;
using System.Data;

/// <summary>
/// Summary description for Contact
/// </summary>
namespace BusinessObjects
{
    public class Contact
    {
        #region Constructors
        public Contact()
        {
        }
        #endregion

        #region Variables
        private int _ContactID;
        private string _ContactCode ;
        private int _MemberID = 0;
        private string _Name ;
        private string _Surname ;
        private string _Title ;
        private string _Phone ;
        private string _GSM ;
        private int _CustomerID = 0;
        private int _AddressID = 0;
        private byte _Gender = 0;
        private string _Email ;
        private string _Description ;
        private DateTime _CreateDate = System.DateTime.Now;
        private DateTime _UpdateDate ;

        #endregion

        #region Properties
        public int ContactID { get { return _ContactID; } set { _ContactID = value; } }
        public string ContactCode { get { return _ContactCode; } set { _ContactCode = value; } }
        public int MemberID { get { return _MemberID; } set { _MemberID = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Surname { get { return _Surname; } set { _Surname = value; } }
        public string Title { get { return _Title; } set { _Title = value; } }
        public string Phone { get { return _Phone; } set { _Phone = value; } }
        public string GSM { get { return _GSM; } set { _GSM = value; } }
        public int CustomerID { get { return _CustomerID; } set { _CustomerID = value; } }
        public int AddressID { get { return _AddressID; } set { _AddressID = value; } }
        public byte Gender { get { return _Gender; } set { _Gender = value; } }
        public string Email { get { return _Email; } set { _Email = value; } }
        public string Description { get { return _Description; } set { _Description = value; } }
        public DateTime CreateDate { get { return _CreateDate; } set { _CreateDate = value; } }
        public DateTime UpdateDate { get { return _UpdateDate; } set { _UpdateDate = value; } }

        #endregion
    }
}