using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FP.CORE.Models;

namespace FP.CORE.DAL
{
    public class FP_DbInitializer: CreateDatabaseIfNotExists<FP_EFContext>
    {
        protected override void Seed(FP_EFContext db)
        {
            try
            {
                // 預設使用者
                new List<FP_USER>
                {
                    new FP_USER { ACCOUNT="9999",CODE="9999",CNAME="超級管理員", ENAME = "SuperUser", EMAIL = "jeffjuan@86shop.com.tw",PW="P@ssw0rd", USERNO ="UA99999999",DEPARTMENTCODE="10000",CreDateTime = null, ENABLE = true }

                }.ForEach(a => db.USER.Add(a));

                // 預設角色
                new List<FP_ROLE>
                {
                    new FP_ROLE { ROLECODE="0", NAME="SuperAdmin",CNAME="超級管理員"},
                    new FP_ROLE { ROLECODE="1", NAME="Admin",CNAME="主管理員"},
                    new FP_ROLE { ROLECODE="2", NAME="Manager",CNAME="管理員"},
                    new FP_ROLE { ROLECODE="3", NAME="Editor",CNAME="編輯者"},
                    new FP_ROLE { ROLECODE="4", NAME="Viewer",CNAME="檢視者"}
                }.ForEach(a => db.ROLE.Add(a));

                // 預設部門
                new List<FP_DEPARTMENT>
                {
                    new FP_DEPARTMENT { NAME="董事長室",CODE="10000"},
                    new FP_DEPARTMENT { NAME="總經理室",CODE="10100"},
                    new FP_DEPARTMENT { NAME="稽核室",CODE="10200"},
                    new FP_DEPARTMENT { NAME="品牌事業處",CODE="20000"},
                    new FP_DEPARTMENT { NAME="品牌營銷部",CODE="20600"},
                    new FP_DEPARTMENT { NAME="品牌企劃部",CODE="20700"},
                    new FP_DEPARTMENT { NAME="商品開發部",CODE="20800"},
                    new FP_DEPARTMENT { NAME="品牌採購部",CODE="20900"},
                    new FP_DEPARTMENT { NAME="代理品牌部",CODE="21000"},
                    new FP_DEPARTMENT { NAME="網路行銷處",CODE="30000"},
                    new FP_DEPARTMENT { NAME="自營商務部",CODE="30100"},
                    new FP_DEPARTMENT { NAME="數位媒體行銷部",CODE="30700"},
                    new FP_DEPARTMENT { NAME="海外行銷部",CODE="30800"},
                    new FP_DEPARTMENT { NAME="海外商務部",CODE="30500"},
                    new FP_DEPARTMENT { NAME="整合行銷部",CODE="30900"},
                    new FP_DEPARTMENT { NAME="客服部",CODE="30600"},
                    new FP_DEPARTMENT { NAME="門市經銷處",CODE="40000"},
                    new FP_DEPARTMENT { NAME="門市行銷部",CODE="40800"},
                    new FP_DEPARTMENT { NAME="北一區",CODE="40400"},
                    new FP_DEPARTMENT { NAME="北二區",CODE="40600"},
                    new FP_DEPARTMENT { NAME="南一區",CODE="40700"},
                    new FP_DEPARTMENT { NAME="南二區",CODE="40900"},
                    new FP_DEPARTMENT { NAME="總管理處",CODE="50000"},
                    new FP_DEPARTMENT { NAME="管理部",CODE="50200"},
                    new FP_DEPARTMENT { NAME="人資部",CODE="50400"},
                    new FP_DEPARTMENT { NAME="資訊處",CODE="60000"},
                    new FP_DEPARTMENT { NAME="軟體開發部",CODE="60100"},
                    new FP_DEPARTMENT { NAME="軟體應用部",CODE="60200"},
                    new FP_DEPARTMENT { NAME="網路部",CODE="60300"},
                    new FP_DEPARTMENT { NAME="物流技術部",CODE="60400"},
                    new FP_DEPARTMENT { NAME="資安部",CODE="60500"},
                    new FP_DEPARTMENT { NAME="物流處",CODE="70000"},
                    new FP_DEPARTMENT { NAME="採購部",CODE="50300"},
                    new FP_DEPARTMENT { NAME="物流部",CODE="70200"},
                    new FP_DEPARTMENT { NAME="訂單部",CODE="70600"},
                    new FP_DEPARTMENT { NAME="儲運一部",CODE="70700"},
                    new FP_DEPARTMENT { NAME="儲運二部",CODE="70800"},
                    new FP_DEPARTMENT { NAME="驗收品保部",CODE="70900"},
                    new FP_DEPARTMENT { NAME="國際貿易處",CODE="80000"},
                    new FP_DEPARTMENT { NAME="企業訂單部",CODE="80100"},
                    new FP_DEPARTMENT { NAME="大陸事業部",CODE="80200"}
                }.ForEach(a => db.DEPARTMENT.Add(a));

                // 預設作業項目
                new List<FP_FEATURE>
                {
                    new FP_FEATURE { NAME="系統設定",CODE="0000",PERMISSION="SuperAdmin,Admin,Manager,Editor,Viewer"},
                    new FP_FEATURE { NAME="使用者管理",CODE="1001",CONTROLLER="User",PERMISSION="SuperAdmin,Admin,Manager,Editor,Viewer",PARENT="0000",URL="/FPUser/User/Index"},
                    new FP_FEATURE { NAME="部門管理",CODE="1002",CONTROLLER="Department",PERMISSION="SuperAdmin,Admin,Manager,Editor,Viewer",PARENT="0000",URL="/FPDepartment/Department/Index"},
                    new FP_FEATURE { NAME="作業管理",CODE="1003",CONTROLLER="Feature",PERMISSION="SuperAdmin,Admin,Manager,Editor,Viewer",PARENT="0000",URL="/FPFeature/Feature"}
                }.ForEach(a => db.FEATURE.Add(a));

                FP_USER user = new FP_USER();
                user = db.USER.Local.FirstOrDefault();

                new List<FP_USER_FEATURE_ROLE>
                {
                    new FP_USER_FEATURE_ROLE { USER_ID = user.ID, FEATURE_CODE ="0000", ROLE_CODE = "0"},
                    new FP_USER_FEATURE_ROLE { USER_ID = user.ID, FEATURE_CODE ="1001", ROLE_CODE = "0"},
                    new FP_USER_FEATURE_ROLE { USER_ID = user.ID, FEATURE_CODE ="1002", ROLE_CODE = "0"},
                    new FP_USER_FEATURE_ROLE { USER_ID = user.ID, FEATURE_CODE ="1003", ROLE_CODE = "0"}
                }.ForEach(a => db.USER_FEATURE_ROLE.Add(a));

                db.SaveChanges();
                base.Seed(db);
            }
            catch(Exception ex)
            {

            }
           
        }
    }
}
