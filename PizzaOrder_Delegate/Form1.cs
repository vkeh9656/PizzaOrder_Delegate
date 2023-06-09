using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PizzaOrder_Delegate
{
    public partial class Form1 : Form
    {
        public delegate int deleFuncDow_Edge(int i);
        public delegate int deleFuncTopping(string strOrder, int EA);

        public delegate T deleFunc<T>(T i);
        public delegate object deleFunc2(object i); // var, object


        int _iTotalPrice = 0; // 전체 가격을 위한 변수

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            deleFuncDow_Edge deleDow = new deleFuncDow_Edge(Dow);
            deleFuncDow_Edge deleEdge = new deleFuncDow_Edge(Edge);

            deleFuncTopping deleTopping = null;

            int iDowOrder = 0;
            int iEdgeOrder = 0;

            // Dow를 선택
            if (rdoDow1.Checked)
            {
                // Dow(1);
                iDowOrder = 1;
            }
            else if(rdoDow2.Checked)
            {
                // Dow(2);
                iDowOrder = 2;
            }

            // deleDow(iDowOrder);
            

            // Edge를 선택
            if (rdoEdge1.Checked)
            {
                // Dow(1);
                iEdgeOrder = 1;
            }
            else if (rdoEdge2.Checked)
            {
                // Dow(2);
                iEdgeOrder = 2;
            }

            // deleEdge(iEdgeOrder);

            CallbackDelegate(iDowOrder, deleDow);
            CallbackDelegate(iEdgeOrder, deleEdge);

            if(cboxTopping1.Checked) deleTopping += Topping1;
            if (cboxTopping2.Checked) deleTopping += Topping2;
            if (cboxTopping3.Checked) deleTopping += Topping3;

            deleTopping("토핑", (int)numEA.Value);

            lboxOrderCall("---------------------------------");
            lboxOrderCall(string.Format("전체 주문 가격은 {0}원입니다.", _iTotalPrice));

        }

        #region Function

        /// <summary>
        /// 0 : 선택안함,  1 : 오리지널,  2 : 씬
        /// </summary>
        /// <param name="iOrder"></param>
        /// <returns></returns>
        private int Dow(int iOrder)
        {
            string strOrder = string.Empty;
            int iPrice = 0;

            if(iOrder == 1)
            {
                iPrice = 10000;
                strOrder = string.Format("오리지널 도우를 선택하셨습니다. ({0}원)",iPrice.ToString());
            }
            else if(iOrder==2)
            {
                iPrice = 11000;
                strOrder = string.Format("씬 도우를 선택하셨습니다. ({0}원)", iPrice.ToString());
            }
            else
            {
                strOrder = "도우를 선택하지 않았습니다.";
            }
            lboxOrderCall(strOrder);

            return _iTotalPrice = _iTotalPrice + iPrice;
        }


        /// <summary>
        /// 0 : 선택안함,  1 : 리치골드,  2 : 치즈크러스트
        /// </summary>
        /// <param name="iOrder"></param>
        /// <returns></returns>
        private int Edge(int iOrder)
        {
            string strOrder = string.Empty;
            int iPrice = 0;

            if (iOrder == 1)
            {
                iPrice = 1000;
                strOrder = string.Format("리치골드 엣지를 선택하셨습니다. ({0}원)", iPrice.ToString());
            }
            else if (iOrder == 2)
            {
                iPrice = 2000;
                strOrder = string.Format("치즈크러스트 엣지를 선택하셨습니다. ({0}원)", iPrice.ToString());
            }
            else
            {
                strOrder = "엣지를 선택하지 않았습니다.";
            }
            lboxOrderCall(strOrder);

            return _iTotalPrice = _iTotalPrice + iPrice;
        }


        public int CallbackDelegate(int i, deleFuncDow_Edge dFunc)
        {
            return dFunc(i);
        }

        private int Topping1(string order, int iEA)
        {
            string strOrder = string.Empty;
            int iPrice = iEA * 500;

            strOrder = string.Format("소세지 {0} {1} 개를 선택하였습니다. : {2}원 (1EA 500원)", order, iEA, iPrice);
            lboxOrderCall(strOrder);

            return _iTotalPrice = _iTotalPrice + iPrice;
        }

        private int Topping2(string order, int iEA)
        {
            string strOrder = string.Empty;
            int iPrice = iEA * 200;

            strOrder = string.Format("감자 {0} {1} 개를 선택하였습니다. : {2}원 (1EA 200원)", order, iEA, iPrice);
            lboxOrderCall(strOrder);

            return _iTotalPrice = _iTotalPrice + iPrice;
        }

        private int Topping3(string order, int iEA)
        {
            string strOrder = string.Empty;
            int iPrice = iEA * 300;

            strOrder = string.Format("치즈 {0} {1} 개를 선택하였습니다. : {2}원 (1EA 300원)", order, iEA, iPrice);
            lboxOrderCall(strOrder);

            return _iTotalPrice = _iTotalPrice + iPrice;
        }


        private void lboxOrderCall(string strOrder)
        {
            lboxOrder.Items.Add(strOrder);
        }

        #endregion
    }
}
