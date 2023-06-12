using prjMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace prjMvcDemo.Controllers
{
    public class AController : Controller
    {
        static int count = 0;

        public ActionResult uplodDemo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult uplodDemo(HttpPostedFileBase photo)
        {
            photo.SaveAs(@"C:\MVCtest\prjMvcDemo\Images\史萊姆.jpg");
            return View();
        }

        public ActionResult showCountByCookie()
        {
            int count = 0;
            HttpCookie x= Request.Cookies["COUNT"];
            if (x != null)
                count = Convert.ToInt32(x.Value);
            count++;
            x = new HttpCookie("COUNT");
            x.Value = count.ToString();
            x.Expires = DateTime.Now.AddSeconds(20);
            Response.Cookies.Add(x);

            ViewBag.COUNT = count;
            return View();
        }

        public ActionResult showCountBySession()
        {
            int count = 0;
            if (Session["COUNT"] != null)
                count = (int)Session["COUNT"];
            count++;
            Session["COUNT"] = count;
            ViewBag.COUNT = count;
            return View();
        }
        public ActionResult showCount()
        {
            count++;
            ViewBag.COUNT = count;
            return View();
        }
        public string sayHello()
        {
            return "Hello ASP.NET MVC";
        }
        public string testingDelete(int? id)
        {
            if (id == null)
                return "沒有指定id";
            (new CCustomerFactory()).delete((int)id);
            return "刪除資料成功";
        }
        public string testingUpdate()
        {
            CCustomer x = new CCustomer()
            {
                fId = 4,
                //fName = "Selina",
                fPhone = "0930000000",
                //fEmail = "selina@gmail.com",
                fAddress = "Taichung",
                //fPassword = "1234",
            };
            (new CCustomerFactory()).update(x);
            return "修改資料成功";
        }

        public string testingInsert()
        {
            CCustomer x = new CCustomer()
            {
                fName = "Selina",
                //fPhone = "0930000000",
                fEmail = "selina@gmail.com",
                //fAddress = "Taichung",
                fPassword = "1234",
            };
            (new CCustomerFactory()).create(x);
            return "新增資料成功";
        }

        public string testingQuery()
        {
            return "目前客戶數：" + (new CCustomerFactory()).queryAll().Count.ToString();
        }
       public ActionResult demoForm()
        {
            ViewBag.ANS = "?";
            if (!string.IsNullOrEmpty(Request.Form["txtA"]))
            {
                double a = Convert.ToDouble(Request.Form["txtA"]);
                double b = Convert.ToDouble(Request.Form["txtB"]);
                double c = Convert.ToDouble(Request.Form["txtC"]);
                ViewBag.a = a;
                ViewBag.b = b;
                ViewBag.c = c;
                double d = System.Math.Sqrt(b*b-4*a*c);

                ViewBag.ANS =((-b+d)/(2*a)).ToString("0.0#")+" Or X="+((-b-d)/(2*a)).ToString();
            }
            return View();
        }

        public ActionResult bindingByFid(int? id)
        {
            CCustomer x = null;
            if (id != null)//判斷id如為空直回傳沒有指定
            {
                SqlConnection con = new SqlConnection();//資料連結
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";//檢視/伺服器總管/新增連線/屬性/連接字串
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
                SqlDataReader reader = cmd.ExecuteReader();//ExecuteReader同步資料庫，ExecuteReaderAsync非同步需要拿號碼牌讀取資料

                if (reader.Read())//如果條件符合讀取資料庫的名欄位和手機欄位
                {
                    x = new CCustomer()
                    {
                        fId = (int)reader["fId"],
                        fName = reader["fName"].ToString(),
                        fPhone = reader["fPhone"].ToString()
                    };
                }
                con.Close();
            }
            return View(x);
        }
        public ActionResult showByFid(int? id)
        {
            //ViewBag.kk = "沒有符合條件的資料";
            //ViewBag.cc = "沒有符合條件的資料";
            //ViewBag.img = "../images/anonymous.jpg";
            if (id != null)//判斷id如為空直回傳沒有指定
            {
                SqlConnection con = new SqlConnection();//資料連結
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";//檢視/伺服器總管/新增連線/屬性/連接字串
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM tCustomer WHERE fId=" + id.ToString(), con);
                SqlDataReader reader = cmd.ExecuteReader();//ExecuteReader同步資料庫，ExecuteReaderAsync非同步需要拿號碼牌讀取資料

                if (reader.Read())//如果條件符合讀取資料庫的名欄位和手機欄位
                {
                    CCustomer x = new CCustomer()
                    {
                        fId = (int)reader["fId"],
                        fName = reader["fName"].ToString(),
                        fPhone = reader["fPhone"].ToString()
                    };
                ViewBag.kk= x;
                }
                con.Close();
            }  
            return View();
        }

        public string queryByFid(int? id)//資料庫查詢
        {
            if (id == null)//判斷id如為空直回傳沒有指定
                return "沒有指定 id";
            SqlConnection con = new SqlConnection();//資料連結
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";//檢視/伺服器總管/新增連線/屬性/連接字串
            con.Open();

            SqlCommand cmd=new SqlCommand("SELECT * FROM tCustomer WHERE fId="+id.ToString(), con);
            SqlDataReader reader = cmd.ExecuteReader();//ExecuteReader同步資料庫，ExecuteReaderAsync非同步需要拿號碼牌讀取資料
            string s = "沒有符合條件的資料";//預設為值為沒有符合條件的資料
            if (reader.Read())//如果條件符合讀取資料庫的名欄位和手機欄位
            {
                s = reader["fName"].ToString() + "/" + reader["fPhone"].ToString();
            }
            con.Close();
            return s;
        }

        //public List<int> LottoNumber()
        //{
        //    //宣告number為產生1~49亂數list
        //   var numbers = Enumerable.Range(1, 49).ToList();
        //    //宣告radom為亂數
        //   var random = new Random();
        //    //產生6個亂數，由小排到大
        //    //1.使用 OrderBy 方法和 random.Next() 函式對 numbers 集合進行隨機排序，OrderBy 方法會依序取出 numbers 集合中的每個元素，再呼叫 random.Next() 函式來隨機排序這些元素，最後回傳一個新的序列。
        //    //2.使用 Take 方法從第一步得到的序列中取出前 6 個元素，這 6 個元素是隨機的，不重複的。
        //    //3.使用 OrderBy 方法按照升序排序第二步得到的 6 個元素，這樣就可以得到一個按照升序排序、不重複的 6 個數字的 List<int>。
        //    var result = numbers.OrderBy(x => random.Next()).Take(6).OrderBy(x=>x).ToList();
        //    return result;
        //}

        //public string Lotto()
        //{
        //    var numbers = LottoNumber();
        //    var result = string.Join(", ", numbers);
        //    return result;
        //}

        public string lotto()//亂數
        {
            return (new CLottoGen()).getNumber();
        }
        public string demoParameter(int? id)//購物車
        {
            if (id == 0)
                return "XBOX加入購物車";
            else if (id == 1)
                return "PS5加入購物車";
            else if (id == 2)
                return "SWITCH加入購物車";
            return "找不到該產品資料";
        }

        public string demoRequest()
        {
            string id = Request.QueryString["Pid"];
            if (id == "0")
                return "XBOX加入購物車";
            else if (id == "1")
                return "PS5加入購物車";
            else if (id == "2")
                return "SWITCH加入購物車";
            return "找不到該產品資料";
        }

        public string demoSever()
        {
            return "目前伺服器的實體位置：" + Server.MapPath(".");
        }

        // GET: A
        public ActionResult Index()
        {
            return View();
        }
    }
}