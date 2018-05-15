namespace DAL
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.IO;

	public class AuctionContent : DbContext
	{
		public AuctionContent()
			: base("name=Model")
		{
		}
		public virtual DbSet<Person> Persons { get; set; }
		public virtual DbSet<Lot> Lots { get; set; }
		public virtual DbSet<LotHistory> History { get; set; }
	}
	public class Person
	{
		public Person()
		{
			Lots = new List<Lot>();
			Histories = new List<LotHistory>();
		}
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string Email { get; set; } // буде як логін
		public string Password { get; set; }

		public virtual ICollection<Lot> Lots { get; set; }
		public virtual ICollection<LotHistory> Histories { get; set; }
	}

	public class Lot
	{
		public Lot()
		{
			History = new List<LotHistory>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public string About { get; set; }
		public int StartPrice { get; set; } // пропоную не гратися з копійками
		public string Photo { get; set; } // тут я не вкурсі як та фотка буде закідатися від клієнта на сервер, а потім по всіх клієнтах
		public DateTime TimeStart { get; set; }
		public DateTime TimeFinish { get; set; }

		public Person WhoSale { get; set; }
		public virtual ICollection<LotHistory> History { get; set; }
	}

	public class LotHistory  // без цього не вийде обійтися 
	{
		public int Id { get; set; }
		public Lot Lot { get; set; }
		public Person Persson { get; set; }
		public int Money { get; set; }
	}



	public class Log  // for exceptions
	{
		public static void Logger(string m)
		{
			using (StreamWriter s = new StreamWriter(@"D:\Exeptions.txt", true))
			{
				s.WriteLine(DateTime.Now + ": " + m);
			}
		}
	}
}