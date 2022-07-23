using LMS_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Project.Logics
{
    public class BorrowLogics
    {
        LMSContext db;

        public BorrowLogics()
        {
            db = new LMSContext();
        }
        public List<Borrow> GetAllBor()
        {
            return db.Borrows.ToList();
        }
        public List<Borrow> GetAllBorByUid(int uid)
        {
            return db.Borrows.Where(br => br.UId == uid).ToList();
        }
        public List<BorrowDetail> GetAllDetail()
        {
            return db.BorrowDetails.ToList();
        }
        public List<BorrowDetail> GetAllDetailByBid(int bid)
        {
            return db.BorrowDetails.Where(br => br.BId == bid).ToList();
        }
        public List<BorrowDetail> GetAllDetailByBorid(int brid)
        {
            return db.BorrowDetails.Where(br => br.BrId == brid).ToList();
        }
    }
}
