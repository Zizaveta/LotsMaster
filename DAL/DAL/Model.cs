namespace DAL
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;

	public class Model : DbContext
	{
		public Model()
			: base("name=Model")
		{
		}

		// public virtual DbSet<MyEntity> MyEntities { get; set; }
	}
	public class Person
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string Email { get; set; } // буде як логін
		public string Password { get; set; }
	}

	public class Lot
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string About { get; set; }
		public int StartPrice { get; set; } // пропоную не гратися з копійками
		public string Photo { get; set; } // тут я не вкурсі як та фотка буде закідатися від клієнта на сервер, а потім по всіх клієнтах
		public DateTime TimeStart { get; set; }
		public DateTime TimeFinish { get; set; }
		public bool IsForAllNow { get; set; } // if (DateTime.Now >TimeStart && DateTime.Now < TimeFinish) return true

		public Person WhoSale { get; set; }
		public Person WhoBuy { get; set; }
		public virtual ICollection<LotHistory> History { get; set; }
	}

	public class LotHistory  // без цього не вийде обійтися 
	{
		public int Id { get; set; }
		public Person Persson { get; set; }
		public int Money { get; set; }
	}
}