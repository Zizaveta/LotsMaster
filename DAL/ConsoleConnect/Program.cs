using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace ConsoleConnect
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DAL.AuctionContent db = new AuctionContent())
            {
                db.Persons.Add(new Person()
                {
                    FirstName ="asd",
                    Password="123123",

                }
                    );
        
                db.SaveChanges();
            }
        }
    }
}
