using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StandardApi.Domain
{
    public class Tag
    {
        public Tag()
        {
            Messages = new HashSet<Message>();
        }

        [Key]
        public Guid Id { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
