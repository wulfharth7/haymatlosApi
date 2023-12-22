using System;
using System.Collections.Generic;

namespace haymatlosApi.haymatlosApi.Models;

public partial class Post
{
    public Guid PkeyUuidPost { get; set; }

    public string? Title { get; set; }

    public Guid? FkeyUuidUser { get; set; }

    public DateTime? RegDate { get; set; }
    
    public string? content { get; set; }

    public string? imageUrl { get; set; }

    public string? category { get; set; }

    public short? Like { get; set; }

    public short? Dislike { get; set; }
    //category
   
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User? FkeyUuidUserNavigation { get; set; }
}
