using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.BL.LibrarySystem.ViewModel;
using LibrarySystem.DAL;

namespace LibrarySystem.BL.LibrarySystem.Business
{
    public class ReservationBusiness
    {
        LibraryDBEntities context = new LibraryDBEntities();
       
        public string SaveReservatin(RESERVATIONVM model)
        {

            if (model.reservation_id == 0)
            {
                TBL_RESRVATION resrvation = new TBL_RESRVATION();
                var book = context.TBL_BOOK.SingleOrDefault(s => s.book_id == model.book_id);
                int NumOFBook = context.TBL_RESRVATION.Where(s => s.user_id == model.user_id).Count();

                
                
                    if (NumOFBook < 5)
                    {
                        resrvation.user_id = model.user_id;
                        resrvation.book_id = model.book_id;
                        context.TBL_RESRVATION.Add(resrvation);

                        book.numofcopies--;

                        context.SaveChanges();
                        return "scusses";
                        // context.TBL_RESRVATION.Remove(resrvation);

                    }
                    else
                        return "the max number of for you is 5 ";
                
                
                   
            }

            return "";
            }
    

        public List<BOOKVM> getBooksOFUser(int id)
        {
            var model = context.TBL_RESRVATION.Where(s => s.user_id == id);
            return model.Select(s =>  new BOOKVM()
            {
                book_id = s.book_id,
                title = s.TBL_BOOK.title,
                isbn = s.TBL_BOOK.isbn,
                numofcopies = s.TBL_BOOK.numofcopies,
                photo = s.TBL_BOOK.photo,

            }).ToList();

        }
        public void DeleteReservation(int id)
        {
            
            var res = context.TBL_RESRVATION.SingleOrDefault(s=> s.reservation_id==id);
           
            if (res != null)
            {
                var book = context.TBL_BOOK.SingleOrDefault(s => s.book_id == res.book_id);
                book.numofcopies++;
                context.TBL_RESRVATION.Remove(res);
                context.SaveChanges();
            }

        }
    }
}
