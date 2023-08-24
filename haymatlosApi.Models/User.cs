using System;
using System.Collections.Generic;

namespace haymatlosApi.haymatlosApi.Models;

public partial class User
{
    public Guid Uuid { get; set; }

    public string? Nickname { get; set; }

    public string? Password { get; set; }

    public string? Salt { get; set; }

    public string? Role { get; set; }

    public string? Token { get; set; }

    public DateTime? RegDate { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
