using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankManagement.Models.EF;
namespace BankManagement.Models.DAO
{
    public class GetAddressDAO
    {
        private static GetAddressDAO _Instance;
        public static GetAddressDAO Instance
        {
            get { return _Instance ?? (_Instance = new GetAddressDAO()); }
            set { }

        }
        public List<DISTRICT> ListDistric { get; set; }
        public List<PROVINCE> ListProvince { get; set; }
        // Hàm khởi tạo
        private GetAddressDAO()
        {
            ListProvince = BankManagementDbContext.Instance.PROVINCEs.ToList();
            ListDistric = BankManagementDbContext.Instance.DISTRICTs.ToList();
        }
        // Lấy danh sách quận huyện khi biết tên tỉnh thành
        public dynamic GetNameDistrictByProvince(string Province)
        {
            List<string> data = new List<string>();

            int IdProvince = GetIDProvinceByNameProvince(Province);
            foreach (var i in ListDistric)
            {
                if (i.ID_Province == IdProvince)
                {
                    data.Add(i.NameDistrict);
                }
            }
            return data;
        }
        // Lấy ID tỉnh bằng tên tỉnh
        public int GetIDProvinceByNameProvince(string NameProvince)
        {
            int index = ListProvince.FindIndex(p => p.NameProvince == NameProvince);
            return ListProvince[index].ID;
        }
        private List<AGENCY> GetAllAddressChiNhanh()
        {
            return BankManagementDbContext.Instance.AGENCies.ToList();
        }
        private string GetNameProvinceByID(int ID)
        {
            return BankManagementDbContext.Instance.PROVINCEs.Find(ID).NameProvince;
        }
        private string GetNameDistrictByID(int IDT, int IDH)
        {
            return BankManagementDbContext.Instance.DISTRICTs.Where(p => p.ID_Province == IDT & p.ID_District == IDH).SingleOrDefault().NameDistrict;
        }
        public List<string> GetAddress()
        {
            List<string> data = new List<string>();
            foreach (var i in GetAllAddressChiNhanh())
            {
                data.Add(i.AddressAgenCy + ", " + GetNameDistrictByID(Convert.ToInt32(i.ID_Province), Convert.ToInt32(i.ID_District)) + ", " + GetNameProvinceByID(Convert.ToInt32(i.ID_Province)));
            }
            return data;
        }
    }
}