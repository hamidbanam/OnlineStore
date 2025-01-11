using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Model.CountactUs
{
    public class ContactUs
    {
        [Key]
        public int ContactId { get; set; }
        public string Title { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public string? Answer { get; set; }

        public int? AnswerUserId { get; set; }

        public string Ip { get; set; }

        #region Relations
        [ForeignKey(nameof(AnswerUserId))]
        public User.User.User? AnswerUser { get; set; }
        #endregion
    }
}
