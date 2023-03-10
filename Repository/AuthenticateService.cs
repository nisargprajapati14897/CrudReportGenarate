using CrudReportGenerate.Interface;
using CrudReportGenerate.Model.Common;
using CrudReportGenerate.Model.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CrudReportGenerate.Repository
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSetting _appSettings;
        public AuthenticateService(IOptions<AppSetting> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        //private List<User> users = new List<User>()
        //{
        //    new User{UserId = 1,FirstName="Nisarg",LastName ="prajapati",
        //        UserName="Nisu@gmail.com",Password="1234"}
        //};

        public List<User> GetLogindetails()
        {
            List<User> Items = new List<User>();
            try
            {
                using (var dBContext = new CustomerReportContext())
                {
                    User cust;
                    foreach (var it in dBContext.User)
                    {
                        cust = new User();
                        cust.UserId = it.UserId;
                        cust.FirstName = it.FirstName;
                        cust.LastName = it.LastName;
                        cust.UserName = it.UserName;
                        cust.Password = it.Password;
                        cust.Dob = it.Dob;
                        cust.Gender = it.Gender;
                        cust.Usertype = it.Usertype;
                        cust.Region = it.Region;
                        Items.Add(cust);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Items;
        }

        public int Registration(Userdata UserModel,string UserName)
        {
            List<Userdata> Items = new List<Userdata>();

            int returnVal = 0;
            try
            {
                using (var dBContext = new CustomerReportContext())
                {
                    Userdata get;
                    foreach (var it in dBContext.User)
                    {
                        get = new Userdata();
                        get.UserId = it.UserId;
                        get.FirstName = it.FirstName;
                        get.LastName = it.LastName; 
                        get.UserName = it.UserName; 
                        get.Password = it.Password;
                        get.Dob = it.Dob;
                        get.Gender = it.Gender;
                        get.Usertype = it.Usertype;
                        get.Region = it.Region;
                        
                        Items.Add(get);
                    }

                    User Cust;
                    //Add record
                    if (UserModel.UserId == 0)
                    {
                        Cust = new User();
                        Cust.FirstName = UserModel.FirstName;
                        Cust.LastName = UserModel.LastName;
                        Cust.UserName = UserModel.UserName;
                        Cust.Password = UserModel.Password;
                        Cust.Dob = UserModel.Dob;
                        Cust.Gender = UserModel.Gender;
                        Cust.Usertype = UserModel.Usertype;
                        Cust.Region = UserModel.Region;
                        dBContext.User.Add(Cust);

                        UserName = Cust.UserName;
                    }


                    bool username = Items.Any(Items => Items.UserName == UserName);
                    if (username == true)
                    {
                        returnVal = -1;
                    }

                    else
                    {
                        //dBContext.TblCustomer.Add(Cust);
                        returnVal = dBContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return returnVal;
        }

        public int Editprofile(Userdata UserModel, string UserName,int UserId)
        {
            List<Userdata> Items = new List<Userdata>();

            int returnVal = 0;
            try
            {
                using (var dBContext = new CustomerReportContext())
                {
                    Userdata get;
                    foreach (var it in dBContext.User)
                    {
                        get = new Userdata();
                        get.UserId = it.UserId;
                        get.FirstName = it.FirstName;
                        get.LastName = it.LastName;
                        get.UserName = it.UserName;
                        get.Password = it.Password;
                        get.Dob = it.Dob;
                        get.Gender = it.Gender;
                        get.Usertype = it.Usertype;
                        get.Region = it.Region;
                        Items.Add(get);
                    }

                    User emp = new User();
                    //Add record

                    emp = dBContext.User.FirstOrDefault(asd => asd.UserId == UserModel.UserId);
                    if (emp != null)
                    {
                        //emp = new Employes();
                        emp.UserId = UserModel.UserId;
                        emp.FirstName = UserModel.FirstName;
                        emp.LastName = UserModel.LastName;
                        emp.UserName = UserModel.UserName;
                        emp.Password = UserModel.Password;
                        emp.Dob = UserModel.Dob;
                        emp.Gender = UserModel.Gender;
                        emp.Usertype = UserModel.Usertype;
                        emp.Region = UserModel.Region;
                        dBContext.User.Update(emp);
                        UserId = emp.UserId;
                        UserName = emp.UserName;
                    }

                    bool username = Items.Any(asd => asd.UserName == UserName);
                    bool usernameexist = Items.Any(asd => (asd.UserId == UserId) && (asd.UserName == UserName));
                    if (usernameexist == true)
                    {
                        returnVal = dBContext.SaveChanges();
                    }
                    else if (username == true)
                    {
                        returnVal = -1;
                    }
                    else
                    {
                        returnVal = dBContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return returnVal;
        }

        public Userdata GetEditprofileById(int UserId)
        {
            Userdata Cust = new Userdata();
            try
            {
                using (var dBContext = new CustomerReportContext())
                {
                    var dep = dBContext.User.Where(x => x.UserId == UserId).SingleOrDefault();

                    if (dep != null)
                    {
                        Cust.UserId = dep.UserId;
                        Cust.FirstName = dep.FirstName;
                        Cust.LastName = dep.LastName;
                        Cust.UserName = dep.UserName;
                        Cust.Password = dep.Password;
                        Cust.Dob = dep.Dob;
                        Cust.Gender = dep.Gender;
                        Cust.Usertype = dep.Usertype;
                        Cust.Region = dep.Region;
                    }
                    return Cust;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public Userdata Authenticate(Userdata Model)
        {

            List<Userdata> users = new List<Userdata>();

            using (var dbContext = new CustomerReportContext())
            {
                Userdata cust;
                foreach (var it in dbContext.User.ToList())
                {
                    cust = new Userdata();
                    cust.UserName = it.UserName;
                    cust.Password = it.Password;
                    cust.FirstName = it.FirstName;
                    users.Add(cust);
                }
            }

            var user = users.SingleOrDefault(x => x.UserName == Model.UserName && x.Password == Model.Password);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Version, "V3.1")
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.Password = null;

            return user;
        }
    }
}