﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Helpers;

namespace BusinessLayer.Concrete
{

    public class AdminManager : IAdminService
    {
        IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public void AdminAdd(Admin p)
        {
            _adminDal.Insert(p);
        }

        public Admin AdminUsername(Admin p)
        {
          var usernameControl = _adminDal.GetByID(x => x.AdminUserName == p.AdminUserName);
            if (usernameControl != null)
            {
                return usernameControl;
            }
            return null;
        }


        public string AdminGenerateSalt()
        {
            string salt = Crypto.GenerateSalt();
            return salt;
        }

        public void AdminHasher(Admin p,string salt)
        {
            
           
            p.PasswordSalt = salt;
            p.AdminPassword = p.AdminPassword + salt;
            p.AdminPassword = Crypto.HashPassword(p.AdminPassword);
            
        }

        public void AdminUpdate(Admin admin)
        {
            _adminDal.Update(admin);
        }

        public Admin getByUsername(string username)
        {
            return _adminDal.GetByID(x => x.AdminUserName==username);
        }

        public bool AdminEncryption(Admin inputValue,Admin dbValue)
        {
            string salt = dbValue.PasswordSalt;
            string plainPass = inputValue.AdminPassword;
            string hashedPass = dbValue.AdminPassword;
            plainPass = plainPass + salt;
            return Crypto.VerifyHashedPassword(hashedPass, plainPass);
        }

        public List<Admin> GetListAdmins()
        {
            return _adminDal.List();
        }

        public void AdminDelete(Admin p)
        {
            if (p.AdminStatus == true)
            {
                p.AdminStatus = false;
            }
            else
            {
                p.AdminStatus = true;
            }
            AdminUpdate(p);
        }

        public Admin GetById(int id)
        {
            return _adminDal.GetByID(x=>x.AdminID==id);
        }

        public string AdminRole(string AdminUserName)
        {
            
            var admin = _adminDal.GetByID(x => x.AdminUserName == AdminUserName);
            return admin.AdminRole;
        }

        public List<Admin> GetListAdminsByRole(string role)
        {
            return _adminDal.List(x=>x.AdminRole==role);
        }
    }
}
