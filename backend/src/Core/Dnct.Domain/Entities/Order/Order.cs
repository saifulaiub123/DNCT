﻿using Dnct.Domain.Common;

namespace Dnct.Domain.Entities.Order;

public class Order:BaseEntity
{
    public string OrderName { get; set; }
    public bool IsDeleted { get; set; }

    #region Navigation Properties

    public User.User User { get; set; }
    public int UserId { get; set; }

    #endregion
}