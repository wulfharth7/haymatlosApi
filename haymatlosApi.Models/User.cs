using System;
using System.Collections.Generic;

namespace haymatlosApi.Models;

public partial class User
{
    public Guid Uuid { get; set; }

    public string? Nickname { get; set; }

    public bool? IsIndexed { get; set; }

    public string? Password { get; set; }

    public string? Salt { get; set; }

    public string? Role { get; set; }

    public string? Token { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
