using Reagentes.Models;

namespace Reagentes
{
    public static class IdentityDataInitializer
    {
        public static void seedRoles(RoleManager<IdentityRole> roleManager, string rolename)
        {
            if(!roleManager.RoleExistsAsync(rolename).Result)
            {
                IdentityRole role = new()
                {
                    Name = rolename
                };

                IdentityResult result = roleManager.CreateAsync(role).Result;

                if(!result.Succeeded)
                {
                    System.Diagnostics.Debug.WriteLine(result.ToString());
                }
            }
        }

        public static void seedUsers(UserManager<IdentityUser> userManager, DataContext context, string 用户名, string 名字, 
            string 电话号码, string 密码, string 角色, string 邮件)
        {
            if(userManager.FindByNameAsync(用户名).Result == null)
            {
                try
                {
                    context.Database.ExecuteSqlInterpolatedAsync($"delete from reagentes.试剂用户 where 工号 = 0");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }

                IdentityUser 身份 = new()
                {
                    UserName = 用户名,
                    Email = 邮件
                };

                IdentityResult result = userManager.CreateAsync(身份, 密码).Result;

                if(result.Succeeded)
                {
                   result = userManager.AddToRoleAsync(身份, 角色).Result;

                    if (result.Succeeded)
                    {

                        试剂用户模型 用户 = new()
                        {
                            用户名 = 用户名,
                            工号 = 0,
                            名字 = 名字,
                            角色 = 角色,
                            身份证号 = "无现货",
                            邮件 = 邮件,
                            电话号码 = 电话号码,
                            注册日期 = DateTime.Now
                        };

                        try
                        {
                            context.Database.ExecuteSqlInterpolatedAsync($"insert into reagentes.试剂用户(用户,工号,用户名,名字,角色,身份证号,邮件,电话号码,注册日期) values(null,{用户.工号},{用户.用户名},{用户.名字},{用户.角色},{用户.身份证号},{用户.邮件},{用户.电话号码},{用户.注册日期})");
                        }
                        catch(Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine(e);
                        }
                     }
                }
            }
        }
    }
}
