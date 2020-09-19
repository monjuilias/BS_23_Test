using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspNet.DTO.Model
{
    public class Comments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentsID { get; set; }
        public string CommentContent { get; set; }
        public int PostID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public int NumberOfLike { get; set; }
        public int NumberOfDisLike { get; set; }

        public Post Post { get; set; }
        public List<Vote> Vote { get; set; }
    }
}
