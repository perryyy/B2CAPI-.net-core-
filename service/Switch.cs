using B2CAPI.Models;
using B2CAPI.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace B2CAPI.service
{

    public class Switch
    {
        private readonly B2C_0322Context _B2C_0322Context;
        public Switch(B2C_0322Context Perry)
        {
            _B2C_0322Context = Perry;
        }

        //performance--回傳商品販賣數量
        public int PrintSum(DateTime Btime, DateTime Etime, int PID)
        {
            return (from od in _B2C_0322Context.OrderDetails
                    where od.PID == PID && od.btime >= Btime && od.btime <= Etime.AddDays(1)
                    group od by od.PID into g
                    select g.Sum(s => s.Prod_qty)
                    ).Sum();
        }
        //AdmCurrentOrder--回傳目前訂單
        public IEnumerable<CurrentOrderDTO> PrintNowOrder()
        {
            return (from o in _B2C_0322Context.Orders
                    where o.order_status == "N"
                    select new CurrentOrderDTO
                    {
                        OID = o.OID,
                        buyer = (from m in _B2C_0322Context.Members
                                 where m.MID == o.MID
                                 select m.username).Single(),
                        //buyer =o.MID,
                        total = o.total,
                        order_time = o.btime.ToString("F")
                    }
                          );
        }

        //AdmOrder--回傳歷史訂單
        public IEnumerable<HistoryOrder> PrintHistoryOrder()
        {
            return (from o in _B2C_0322Context.Orders
                    where o.order_status == "Y"
                    select new HistoryOrder
                    {
                        OID = o.OID,
                        buyer = (from m in _B2C_0322Context.Members
                                 where m.MID == o.MID
                                 select m.username).Single(),
                        total = o.total,
                        order_time = o.btime.ToString("F"),
                        order_status = o.order_status
                    });
        }

        //Param--回傳參數
        public IEnumerable<Param> PrintParam()
        {
            return _B2C_0322Context.Params.Select(x => new Param
            {
                PID = x.PID,
                category = x.category,
                name = x.name,
                status = x.status,
                spare = x.spare,
                priority = x.priority
            });
        }

        //Product--回傳商品+照片list
        public IEnumerable<ProductImgDTO> PrintProductImgDTO()
        {
            return (from p in _B2C_0322Context.Products
                    let i = _B2C_0322Context.Imgs.Where(x => x.PID == p.PID).OrderBy(x => x.PID).FirstOrDefault()
                    where i.Img_name != null
                    select new ProductImgDTO
                    {
                        PID = p.PID,
                        price = p.price,
                        sale = p.sale,
                        name = p.name,
                        category = p.category,
                        qty = p.qty,
                        img = System.IO.File.ReadAllBytes(i.Img_path + i.Img_name)
                    }
                    );
        }
        //Member--回傳所有會員資料
        public IEnumerable<memberDTO> PrintMember()
        {
            return _B2C_0322Context.Members.Select(x => new memberDTO
            {
                Mid = x.MID,
                Username = x.username,
                Sex = x.sex,
                Phone = x.phone,
                Email = x.email,
                Birth = x.birth,
                auth = x.auth
            });
        }
        //AdmMember--回傳所有會員資料(管理員)
        public IEnumerable<AdmMemberDTO> PrintAdmMember()
        {
            return _B2C_0322Context.Members.Select(x => new AdmMemberDTO
            {
                MID = x.MID,
                username = x.username,
                sex = x.sex,
                birth = x.birth,
                phone = x.phone,
                email = x.email,
                auth = x.auth,
                status = x.status,
                startdate = x.startdate,
                enddate = x.enddate
            });
        }
        //Cart--回傳購物車內容(帶id)
        public IEnumerable<CartDTO> PrintCart(int id)
        {
            return (from c in _B2C_0322Context.Carts
                    where c.MID == id
                    select new CartDTO
                    {
                        CID = c.CID,
                        PID = (from p in _B2C_0322Context.Products
                               let i = _B2C_0322Context.Imgs.Where(x => x.PID == p.PID).OrderBy(x => x.PID).FirstOrDefault()
                               where i.Img_name != null && i.PID == c.PID
                               select new CartDetailsDTO
                               {
                                   CID = c.CID,
                                   PID = c.PID,
                                   price = p.price,
                                   sale = p.sale,
                                   name = p.name,
                                   category = p.category,
                                   qty = p.qty,
                                   img = System.IO.File.ReadAllBytes(i.Img_path + i.Img_name)
                               }).ToList(),
                        MID = (from m in _B2C_0322Context.Members
                               where c.MID == m.MID
                               select new Member
                               {
                                   MID = m.MID
                               }).ToList(),
                        qty = c.qty
                    });
        }
        //Cart--回傳購物車內容
        public IEnumerable<CartDTO> PrintCartNoParam()
        {
            return (from c in _B2C_0322Context.Carts
                    select new CartDTO
                    {
                        CID = c.CID,
                        PID = (from p in _B2C_0322Context.Products
                               let i = _B2C_0322Context.Imgs.Where(x => x.PID == p.PID).OrderBy(x => x.PID).FirstOrDefault()
                               where i.Img_name != null && i.PID == c.PID
                               select new CartDetailsDTO
                               {
                                   CID = c.CID,
                                   PID = c.PID,
                                   price = p.price,
                                   sale = p.sale,
                                   name = p.name,
                                   category = p.category,
                                   qty = p.qty,
                                   img = System.IO.File.ReadAllBytes(i.Img_path + i.Img_name)
                               }).ToList(),
                        MID = (from m in _B2C_0322Context.Members
                               where c.MID == m.MID
                               select new Member
                               {
                                   MID = m.MID
                               }).ToList(),
                        qty = c.qty
                    });
        }
        //order--回傳訂單
        public IEnumerable<Order> PrintOrder()
        {
            return _B2C_0322Context.Orders.Select(x => new Order
            {
                OID = x.OID,
                total = x.total,
                MID = x.MID,
                order_date = x.order_date,
                order_status = x.order_status
            });
        }
        //orderdetails--回傳訂單明細
        public IEnumerable<ODPIDTO> PrintOrderDetails(int id)
        {
            return (from o in _B2C_0322Context.OrderDetails
                    let i = _B2C_0322Context.Imgs.Where(x => x.PID == o.PID).OrderBy(x => x.PID).FirstOrDefault()
                    where o.OID == id
                    select new ODPIDTO
                    {
                        OID = o.OID,
                        ODID = o.ODID,
                        PID = o.PID,
                        Prod_name = o.Prod_name,
                        Prod_price = o.Prod_price,
                        Prod_qty = o.Prod_qty,
                        Prod_DID = o.Prod_DID,
                        Prod_sale = o.Prod_sale,
                        img = System.IO.File.ReadAllBytes(i.Img_path + i.Img_name)
                    });
        }
        //product--回傳商品明細
        public Prod_d_DTO PrintProductDetails(int id,int TmpPID)
        {
            return (from p in _B2C_0322Context.Products
                    where p.PID == id
                    select new Prod_d_DTO
                    {
                        PID = TmpPID,
                        price = p.price,
                        sale = p.sale,
                        name = p.name,
                        category = p.category,
                        qty = p.qty,
                        buser = p.buser,
                        btime = p.btime,
                        img = (from d in _B2C_0322Context.Imgs
                               where d.PID == id
                               select System.IO.File.ReadAllBytes(d.Img_path + d.Img_name)).ToList()
                    }
                        ).FirstOrDefault();
        }
    }
}

