﻿using DigitalBookStoreManagement.Models;

namespace DigitalBookStoreManagement.Repository
{
    public interface IUserRepo
    {
        List<User> GetUserInfo();
        User GetUserInfo(int id);

        int AddUser(User userInfo);

        int RemoveUser(int id);

        int UpdateUser(int id, User userInfo);
    }
}
