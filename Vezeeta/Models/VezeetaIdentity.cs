using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Vezeeta.Models
{
    public class VezeetaIdentity : IdentityDbContext<MyIdentityUser>
    {
       
        public VezeetaIdentity():base("MyModel")
        {
          
                
        }
    }
    public class MyIdentityUser : IdentityUser
    {
        //public string Email { get; set; }
        //public bool ConfirmedEmail { get; set; }
    }
}