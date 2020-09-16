using System;

namespace Group10.Data.Models
{
    public class Claims
    {
        public int Id { get; set; }
        public int Claim { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; }
    }

}