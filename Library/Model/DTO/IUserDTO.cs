using System;

namespace Library.Model.DTO
{
    public interface IUserDTO
    {
        DateTime AddBusinessDays(int days);
    }
}