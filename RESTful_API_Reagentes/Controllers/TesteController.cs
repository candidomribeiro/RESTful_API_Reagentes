//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using RESTful_Reagentes.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;

//namespace RESTful_Reagentes.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    [Authorize(Roles = "Admin,User,Technician")]
//    public class TesteController : Controller
//    {
//        private DataContext Context { get; }

//        public TesteController(DataContext ctx)
//        {
//            Context = ctx;
//        }

//        public class Tst
//        {
//            public long A { get; set; }
//            public long B { get; set; }
//        }

//        [HttpGet]
//        public IAsyncEnumerable<MTeste> GetTeste()
//        {
//            return Context.Teste;
//        }

//        //[HttpGet("func")]
//        //public async Task TesteFunc([FromQuery] tst t)
//        //{
//        //    var r = Context.UserCredentialsVerify(t.a, t.b);
//        //    Console.WriteLine(r);
//        //    await Context.SaveChangesAsync();
//        //}


//        [HttpPost]
//        public async Task UpdateTeste([FromBody] MTeste mTeste)
//        {
//            await Context.Teste.AddAsync(mTeste);
//            await Context.SaveChangesAsync();
//        }
       
//    }
//}
