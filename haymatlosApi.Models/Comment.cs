using System;
using System.Collections.Generic;

namespace haymatlosApi.haymatlosApi.Models;

public partial class Comment
{
    public Guid PkeyUuidComment { get; set; }

    public Guid? FkeyUuidPost { get; set; }

    public string? Description { get; set; }

    public bool? IsIndexed { get; set; }

    public Guid? ParentComment { get; set; }

    public virtual Post? FkeyUuidPostNavigation { get; set; }
}
