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

        public delegate T deleFunc<T>(T i); // 함수명 뒤에 <일반화 변수명 - 형식 매개변수>를 사용할 경우 일반화 함수를 지정할 수 있음(인자 값 Type에 신경쓰지 않아도됨.)
        public delegate object deleFunc2(object i); // var, object


        int _iTotalPrice = 0; // 전체 가격을 위한 변수

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> dPizzaOrder = new Dictionary<string, int>(); // Pizza 주문을 담을 그릇

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
                dPizzaOrder.Add("오리지널", 1);
            }
            else if(rdoDow2.Checked)
            {
                // Dow(2);
                iDowOrder = 2;
                dPizzaOrder.Add("씬", 1);
            }

            // deleDow(iDowOrder);
            

            // Edge를 선택
            if (rdoEdge1.Checked)
            {
                // Dow(1);
                iEdgeOrder = 1;
                dPizzaOrder.Add("리치골드", 1);
            }
            else if (rdoEdge2.Checked)
            {
                // Dow(2);
                iEdgeOrder = 2;
                dPizzaOrder.Add("치즈크러스트", 1);
            }

            // deleEdge(iEdgeOrder);

            CallbackDelegate(iDowOrder, deleDow);
            CallbackDelegate(iEdgeOrder, deleEdge);

            // delegate Chain ! (이벤트 추가만 해놓고)
            if(cboxTopping1.Checked)
            {
                deleTopping += Topping1;
                dPizzaOrder.Add("소세지", (int)numEA.Value);
            }

            if (cboxTopping2.Checked)
            {
                deleTopping += Topping2;
                dPizzaOrder.Add("감자", (int)numEA.Value);
            }

            if (cboxTopping3.Checked)
            {
                deleTopping += Topping3;
                dPizzaOrder.Add("치즈", (int)numEA.Value);
            }
                

            // 추가해놨던 이벤트 여기서 한방에 순차적으로 다 호출해서 처리
            deleTopping("토핑", (int)numEA.Value);

            lboxOrderCall("---------------------------------");
            lboxOrderCall(string.Format("전체 주문 가격은 {0}원입니다.", _iTotalPrice));

            formLoading(dPizzaOrder);
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

        #region event 예제
        
        FormPizza _fPizza; // 한번만 생성하기 위해 전역으로 호출
        private void formLoading(Dictionary<string, int> dPizzaOrder)
        {
            if (_fPizza != null)
            {
                _fPizza.Dispose(); // 기존에 폼이 있으면 해제
            }

            _fPizza = new FormPizza();
            _fPizza.eventDelePizzaComplete += _fPizza_eventDelePizzaComplete;
            _fPizza.Show();

            _fPizza.PizzaCheck(dPizzaOrder);
        }

        private int _fPizza_eventDelePizzaComplete(string strResult, int iTime)
        {
            return 0;
        }
        #endregion
    }
}
