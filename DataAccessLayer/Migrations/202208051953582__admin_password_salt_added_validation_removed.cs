﻿namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _admin_password_salt_added_validation_removed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "PasswordSalt", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "PasswordSalt", c => c.String(maxLength: 100));
        }
    }
}
