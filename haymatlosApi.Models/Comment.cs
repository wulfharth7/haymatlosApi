using System;
using System.Collections.Generic;

namespace haymatlosApi.haymatlosApi.Models;

public partial class Comment
{
    public Guid PkeyUuidComment { get; set; }

    public Guid? FkeyUuidPost { get; set; }

    public string? Description { get; set; }

    public Guid? ParentComment { get; set; }

    public DateTime? RegDate { get; set; }
    public string? commenterUsername { get; set; }

    public short? Like { get; set; }

    public short? Dislike { get; set; }

    public Guid? FkeyUuidUser { get; set; }

    public virtual Post? FkeyUuidPostNavigation { get; set; }

    public virtual User? FkeyUuidUserNavigation { get; set; }
}
