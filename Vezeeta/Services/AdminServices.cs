using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vezeeta.Models;

namespace Vezeeta.Services
{
    public interface IAdminService
    {
        bool Login(string Email, string password);
        bool ChangePassWord(string Email, string password);
        bool ForgetPassWord(string Email);
    }
    public class AdminServices : IAdminService
    {
        public MyModel context { get; set; }
        public AdminServices()
        {
            context = new MyModel();
        }
        public bool Login(string Email, string password)
        {
           return context.Admins
                    .Where(a => a.Email == Email && a.PassWord == password)
                    .Any();

        }
        public bool ChangePassWord(string Email, string password)
        {
            throw new NotImplementedException();
        }

        public bool ForgetPassWord(string Email)
        {
            throw new NotImplementedException();
        }

        
    }
}