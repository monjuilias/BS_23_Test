using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspNet.DTO.Model
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostID { get; set; }
        public string PostContent { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }

        public int NumberOfComments { get; set; }

        public List<Comments> Comments { get; set; }



    }
}
