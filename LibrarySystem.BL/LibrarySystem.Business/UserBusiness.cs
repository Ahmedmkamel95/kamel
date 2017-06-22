using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrarySystem.BL.LibrarySystem.ViewModel;
using LibrarySystem.DAL;

namespace LibrarySystem.BL.LibrarySystem.Business
{
  public  class UserBusiness
    {
        
        
      LibraryDBEntities context = new LibraryDBEntities();
      public bool ValidateModel(USERVM model)
      {

          if (string.IsNullOrEmpty(model.username))
              return false;
          if (string.IsNullOrEmpty(model.password))
              return false;
          if (model.username.Length > 40)
              return false;
          if (model.password.Length > 50)
              return false;
         

          return true;

      }
      public USERVM GetUserById(int id)
      {
          //return one recored in database or null
          //delgate function 
          var res = context.TBL_USER.SingleOrDefault(s => s.user_id == id);
          return new USERVM()
          {
              user_id = res.user_id,
              username=res.username,
              password=res.password,
          };
      }
      public void registration(USERVM model)
      {
          bool Isvalid = ValidateModel(model);
          if (Isvalid)
          {
              if (model.user_id == 0)
              {
                  TBL_USER User = new TBL_USER();
                  User.username = model.username;
                  User.password = model.password;
                
                  context.TBL_USER.Add(User);
              }
              else
              {
                  var res = context.TBL_USER.SingleOrDefault(s => s.user_id == model.user_id);
                  res.username = model.username;
                  res.password = model.password;
                 
              }
              context.SaveChanges();

          }
      }

      public int login(USERVM model)
      {

          var res = context.TBL_USER.SingleOrDefault(s => s.username==model.username && s.password==model.password);
          if (res != null)
              return res.user_id;
          else
              return 0;
      }
    }
}
