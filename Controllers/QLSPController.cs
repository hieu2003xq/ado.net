using ado.net.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace ado.net.Controllers
{
    public class QLSPController : Controller
    {
        // GET: QLSP
        public ActionResult Index()
        {
            string strCon = @"Data Source=WINDOWS-11\SQLEXPRESS;Initial Catalog=qlBh;Integrated Security=True";
            var connection = new SqlConnection(strCon);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select*from sanPham";
            var reader=cmd.ExecuteReader();
            List<SP> lst=new List<SP>();
            while (reader.Read())
            {
                string masp = reader["maSP"].ToString();
                string tensp = reader["tenSP"].ToString();
                string giaban = reader["giaBan"].ToString();
                string hinhAnh = reader["hinhAnh"].ToString();
                SP a=new SP()
                {
                    maSP = masp,
                    tenSP = tensp,
                    giaBan = giaban,
                    hinhAnh=hinhAnh,
                };
                lst.Add(a);
            }
            return View(lst);
        }
        public ActionResult themSP()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult themSP(int maSP,string tenSP,string giaBan,string hinhAnh)
        {
            string strCon = @"Data Source=WINDOWS-11\SQLEXPRESS;Initial Catalog=qlBh;Integrated Security=True";
            var connection = new SqlConnection(strCon);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = System.Data.CommandType.Text;
            string sql = "insert into sanPham (maSP,tenSP,giaBan,hinhAnh) values(@ma,@ten,@gia,@hinhAnh)";
            cmd.CommandText = sql;
            cmd.Parameters.Add("@ma", System.Data.SqlDbType.Int).Value = maSP;
            cmd.Parameters.Add("@ten", System.Data.SqlDbType.NVarChar).Value = tenSP;
            cmd.Parameters.Add("@gia", System.Data.SqlDbType.Money).Value = giaBan;
            cmd.Parameters.Add("@hinhAnh", System.Data.SqlDbType.NVarChar).Value = hinhAnh;
            int ret=cmd.ExecuteNonQuery();
            if(ret>0)
            {
                MessageBox.Show("them thang cong");
            }
            else
            {
                MessageBox.Show("them khong thang cong");
            }
            return RedirectToAction("Index","QLSP");
        }
        public ActionResult xoa(int id)
        {
            string strCon = @"Data Source=WINDOWS-11\SQLEXPRESS;Initial Catalog=qlBh;Integrated Security=True";
            var connection = new SqlConnection(strCon);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = System.Data.CommandType.Text;
            string sql = "delete from sanPham where maSP=@ma" ;
            cmd.CommandText = sql;
            cmd.Parameters.Add("@ma", System.Data.SqlDbType.Int).Value = id;
            int ret = cmd.ExecuteNonQuery();
            if(ret>0)
            {
                MessageBox.Show("xoa thang cong");
            }
            else
            {
                MessageBox.Show("xoa khong thang cong");
            }
            return RedirectToAction("Index", "QLSP");
        }
        [HttpGet]
        public ActionResult sua(int id)
        {
            string strCon = @"Data Source=WINDOWS-11\SQLEXPRESS;Initial Catalog=qlBh;Integrated Security=True";
            var connection = new SqlConnection(strCon);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select*from sanPham where maSP=@ma";
            cmd.Parameters.AddWithValue("@ma", id);
            var reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                string masp = reader["maSP"].ToString();
                string tensp = reader["tenSP"].ToString();
                string giaban = reader["giaBan"].ToString();
                string hinhAnh = reader["hinhAnh"].ToString();
                SP a = new SP()
                {
                    maSP = masp,
                    tenSP = tensp,
                    giaBan = giaban,
                    hinhAnh = hinhAnh,
                };
                return View(a);
            }
            return RedirectToAction("Index", "QLSP");
           
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult sua(int maSP, string tenSP, string giaBan, string hinhAnh)
        {
            string strCon = @"Data Source=WINDOWS-11\SQLEXPRESS;Initial Catalog=qlBh;Integrated Security=True";
            var connection = new SqlConnection(strCon);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
                string sql = "update sanPham set tenSP=@ten ,giaBan=@gia,hinhAnh=@hinhAnh where maSP=@ma";
            cmd.CommandText= sql;
            cmd.Parameters.Add("@ma", System.Data.SqlDbType.Int).Value = maSP;
            cmd.Parameters.Add("@ten", System.Data.SqlDbType.NVarChar).Value = tenSP;
            cmd.Parameters.Add("@gia", System.Data.SqlDbType.Money).Value = giaBan;
            cmd.Parameters.Add("@hinhAnh", System.Data.SqlDbType.NVarChar).Value = hinhAnh;
            int ret = cmd.ExecuteNonQuery();
            if (ret > 0)
            {
                MessageBox.Show("sua thang cong");
            }
            else
            {
                MessageBox.Show("sua khong thang cong");
            }
            return RedirectToAction("Index", "QLSP");
        }
    }
}