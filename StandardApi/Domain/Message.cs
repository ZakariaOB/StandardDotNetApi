using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StandardApi.Domain
{
    public class Message
    {
        public Message()
        {
            Tags = new HashSet<Tag>();
        }

        [Key]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User{ get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}