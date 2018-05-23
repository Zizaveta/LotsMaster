namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.IO;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    public class AuctionContent : DbContext
	{
		public AuctionContent()
			: base("name=Model")
		{
		}
		public virtual DbSet<Person> Persons { get; set; }
		public virtual DbSet<Lot> Lots { get; set; }
		public virtual DbSet<LotHistory> History { get; set; }
        public virtual DbSet<Tell> Tells { get; set; }
	}
    [DataContract]
    public class Person
	{
		public Person()
		{
			Lots = new List<Lot>();
			Histories = new List<LotHistory>();
            Tells = new List<Tell>();
		}
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string SecondName { get; set; }
        [DataMember]
        public bool Gender { get; set; }
        [DataMember]
        public byte[] Image { get; set; }
        [DataMember]
        public string Email { get; set; } // буде як логін
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public virtual ICollection<Lot> Lots { get; set; }
        [DataMember]
        public virtual ICollection<LotHistory> Histories { get; set; }
        [DataMember]
        public virtual ICollection<Tell> Tells { get; set; }

    }

    [DataContract]
    public class Lot
	{
		public Lot()
		{
			History = new List<LotHistory>();
            Tells = new List<Tell>();
		}
        [DataMember]
		public int Id { get; set; }
        [DataMember]
        public string LotName { get; set; }
        [DataMember]
        public string About { get; set; }
        [DataMember]
        public int StartPrice { get; set; } 
        [DataMember]
        public byte[] Photo { get; set; } 
        [DataMember]
        public DateTime TimeStart { get; set; }
        [DataMember]
        public DateTime TimeFinish { get; set; }
        [DataMember]
        public virtual Person WhoSale { get; set; }
        [DataMember]
        public virtual ICollection<LotHistory> History { get; set; }
        [DataMember]
        public virtual ICollection<Tell> Tells { get; set; }
	}
    [DataContract]
    public class Tell
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public virtual Person Person { get; set; }
        [DataMember]
        public virtual Lot Lot { get; set; }
    }
    [DataContract]
    public class LotHistory  
	{
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public virtual Lot Lot { get; set; }
        [DataMember]
        public virtual Person Persson { get; set; }
        [DataMember]
        public int Money { get; set; }
	}
}