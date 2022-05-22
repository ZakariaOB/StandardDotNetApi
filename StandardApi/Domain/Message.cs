using System;
using System.ComponentModel.DataAnnotations;

namespace StandardApi.Domain
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string Topic { get; set; }
    }
}