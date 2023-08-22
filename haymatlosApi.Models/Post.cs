using System;
using System.Collections.Generic;

namespace haymatlosApi.haymatlosApi.Models;

public partial class Post
{
    public Guid PkeyUuidPost { get; set; }

    public string? Title { get; set; }

    public Guid? FkeyUuidUser { get; set; }

    public bool? IsIndexed { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User? FkeyUuidUserNavigation { get; set; }
}
