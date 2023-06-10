using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzaOrder_Delegate
{
    public partial class FormPizza : Form
    {
        public delegate int delePizzaComplete(string strResult, int iTime);
        public event delePizzaComplete eventDelePizzaComplete; // delegate event 선언
        

        public FormPizza()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void PizzaCheck(Dictionary<string, int> dPizzaOrder)
        {
            int iTotalTime = 0;

            foreach (KeyValuePair<string, int> oOrder in dPizzaOrder)
            {
                int iCreateTime = 0;
                string strType = string.Empty;
                int iTime = 0;
                int iCount = oOrder.Value;

                switch (oOrder.Key)
                {
                    // 도우
                    case "오리지널":
                        iCreateTime = 3000;
                        strType = "도우";
                        break;
                    case "씬":
                        iCreateTime = 3500;
                        strType = "도우";
                        break;
                    
                    // 엣지
                    case "리치골드":
                        iCreateTime = 500;
                        strType = "엣지";
                        break;
                    case "치즈크러스트":
                        iCreateTime = 400;
                        strType = "엣지";
                        break;

                    // 토핑
                    case "소세지":
                        iCreateTime = 32;
                        strType = "토핑";
                        break;
                    case "감자":
                        iCreateTime = 17;
                        strType = "토핑";
                        break;
                    case "치즈":
                        iCreateTime = 48;
                        strType = "토핑";
                        break;

                    default:
                        break;
                }
                iTime = iCreateTime * iCount;

                iTotalTime = iTotalTime + iTime;

                lboxMake.Items.Add(string.Format("{0}) {1} : {2}초 ({3}초, {4}개)",
                            strType, oOrder.Key, iTime, iCreateTime, oOrder.Value));

                Refresh();
                Thread.Sleep(1000);

            }

            int iRet = eventDelePizzaComplete("Pizza가 완료되었습니다.", iTotalTime);

            lboxMake.Items.Add("------------------------");
            if (iRet == 0)
            {
                lboxMake.Items.Add("주문 완료 확인!");
            }
            else
            {
                lboxMake.Items.Add("제작 시간 초과로 고객 반품");
            }

        }
    }
}
