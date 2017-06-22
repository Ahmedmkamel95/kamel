using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.BL.LibrarySystem.ViewModel;
using LibrarySystem.DAL;

namespace LibrarySystem.BL.LibrarySystem.Business
{
  public  class BOOKBusiness
    {
      LibraryDBEntities context = new LibraryDBEntities();
      public List<BOOKVM> RetrivealBooks()
      {
          var res = context.TBL_BOOK;
          return res.Select(s => new BOOKVM()
          {
              book_id = s.book_id,
              isbn = s.isbn,
              numofcopies = s.numofcopies,
              photo = s.photo,
              title = s.title,
          }).ToList();
      }
       public BOOKVM GetBookById(int id)
      {
       //return one recored in database or null
      //delgate function 
           var res=context.TBL_BOOK.SingleOrDefault(s=>s.book_id==id);
           return new BOOKVM()
           {
               book_id = res.book_id,
               isbn = res.isbn,
               numofcopies = res.numofcopies,
               photo =res.photo,
               title = res.title,
           };
      }
      public bool ValidateModel(BOOKVM model)
       {
          
           if (string.IsNullOrEmpty(model.title))
               return false;
           if (string.IsNullOrEmpty(model.photo))
               return false;
           if (model.title.Length>40)
               return false;
           if (model.photo.Length>245)
               return false;
           if (model.numofcopies < 0)
               return false;
           if(model.isbn<=0)
               return false;

          return true;
       
       }

      public void SaveBook(BOOKVM model)
      {
          bool Isvalid = ValidateModel(model);
          if (Isvalid)
          {
              if (model.book_id == 0)
              {
                  TBL_BOOK book = new TBL_BOOK();

                  book.photo = model.photo;
                  book.title = model.title;
                  book.numofcopies = model.numofcopies;
                  book.isbn = model.isbn;

                  context.TBL_BOOK.Add(book);
              }
               else
              {
                  var res = context.TBL_BOOK.SingleOrDefault(s => s.book_id == model.book_id);
                  res.photo = model.photo;
                  res.title = model.title;
                  res.numofcopies = model.numofcopies;
                  res.isbn = model.isbn;
              }
              context.SaveChanges();

          }
      }
       public void DeleteBook(int id)
      {
          var res = context.TBL_BOOK.Find(id);
           if(res!=null)
           {
               context.TBL_BOOK.Remove(res);
               context.SaveChanges();
           }

      }

      public List<BOOKVM> Search(string title)
       {
           var res = context.TBL_BOOK.Where(s => s.title.Contains(title));
          // var res = context.TBL_BOOK.Where(x => title.Contains(x.title));
           return res.Select(s => new BOOKVM()
           {
               book_id = s.book_id,
               isbn = s.isbn,
               numofcopies = s.numofcopies,
               photo = s.photo,
               title = s.title,
           }).ToList();
          
       }
    }
}
