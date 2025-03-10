﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBookStoreManagement.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required(ErrorMessage = "UserId is Required.")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Order Date is Required.")]
        public DateTime OrderDate { get; set; }
        

        //[Required(ErrorMessage = "Total Amount is Required.")]
        //[Range(0.01, double.MaxValue, ErrorMessage = " Total Amount must be greater than zero")]
        public double TotalAmount { get; set; }


        public string? OrderStatus { get; set; }

        [Required(ErrorMessage = "Payment Status is Required.")]
        public string? PaymentStatus { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
