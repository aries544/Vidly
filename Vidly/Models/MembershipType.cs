using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        [Required]
        public short SingUpFee { get; set; }

        [Required]
        public byte DurationInMonths { get; set; }

        [Required]
        public byte DiscountRate { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }
    }
}